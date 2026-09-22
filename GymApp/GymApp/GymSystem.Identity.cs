using System;

namespace GymApp
{
    public partial class GymSystem
    {
        // התווים המותרים בסיסמה
        private const string Letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Digits = "0123456789";
        private const string Specials = "$%_";

        // טלפון תקין: בדיוק 10 ספרות, ומתחיל ב-05
        private bool IsValidPhone(string phone)
        {
            if (phone == null || phone.Length != 10)
                return false;

            for (int i = 0; i < phone.Length; i++)
            {
                if (phone[i] < '0' || phone[i] > '9')
                    return false;
            }
            return phone[0] == '0' && phone[1] == '5';
        }

        // דוא"ל תקין: יש @ שאינו בתחילת המחרוזת, ואחריו נקודה שאינה התו האחרון
        private bool IsValidEmail(string email)
        {
            if (email == null || email == "")
                return false;

            int at = email.IndexOf('@');
            if (at <= 0)
                return false;

            int dot = email.IndexOf('.', at);
            return dot != -1 && dot != email.Length - 1;
        }

        // סיסמה חזקה: אורך 8-12, לפחות אות, ספרה ותו מיוחד ($ % _), בלי תווים אחרים
        private bool IsStrongPassword(string password)
        {
            if (password == null || password.Length < 8 || password.Length > 12)
                return false;

            bool hasLetter = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            for (int i = 0; i < password.Length; i++)
            {
                char c = password[i];
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                    hasLetter = true;
                else if (c >= '0' && c <= '9')
                    hasDigit = true;
                else if (c == '$' || c == '%' || c == '_')
                    hasSpecial = true;
                else
                    return false;   // תו אסור, כולל רווח
            }
            return hasLetter && hasDigit && hasSpecial;
        }

        // יוצרת סיסמה ראשונית אקראית שעומדת בכללים
        private string GenerateInitialPassword()
        {
            int length = random.Next(8, 13);   // אורך בין 8 ל-12
            char[] chars = new char[length];

            // חובה: אות אחת, ספרה אחת ותו מיוחד אחד
            chars[0] = Letters[random.Next(Letters.Length)];
            chars[1] = Digits[random.Next(Digits.Length)];
            chars[2] = Specials[random.Next(Specials.Length)];

            // משלימים את השאר מכל התווים המותרים
            string all = Letters + Digits + Specials;
            for (int i = 3; i < length; i++)
                chars[i] = all[random.Next(all.Length)];

            // מערבבים כדי שהאות, הספרה והתו המיוחד לא יהיו תמיד בהתחלה
            for (int i = length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = chars[i];
                chars[i] = chars[j];
                chars[j] = temp;
            }

            return new string(chars);
        }


        // REQ-001: יצירת חשבון חדש (Trainer או Trainee) בידי מנהל.
        // מחזירה את המשתמש החדש, או null אם משהו נכשל.
        // הסיסמה הראשונית מוחזרת בנפרד דרך initialPassword.
        public User? CreateAccount(string type, string name, string phone,
                                   string email, out string initialPassword)
        {
            // בכשל לא מחזירים מידע חלקי
            initialPassword = "";

            // 1. בדיקות קלט
            if (type != "Trainer" && type != "Trainee")
                return null;
            if (name == null || name.Trim() == "")
                return null;
            if (!IsValidPhone(phone) || !IsValidEmail(email))
                return null;

            // 2. יש תא פנוי במערך?
            int index = FindFreeUserIndex();
            if (index == -1)
                return null;

            // 3. מזהה ייחודי (אם כבר קיים, מפיקים מזהה נוסף)
            string? id;
            do
            {
                id = GenerateUserId(type);
            }
            while (id == null || FindUserById(id) != null);

            // 4. סיסמה ראשונית שעוברת את בדיקת החוזק
            string password;
            do
            {
                password = GenerateInitialPassword();
            }
            while (!IsStrongPassword(password));

            // 5. רק עכשיו יוצרים ושומרים
            User newUser = new User(id, password, name.Trim(), phone, email, type);
            users[index] = newUser;
            initialPassword = password;
            return newUser;
        }

        // REQ-002: אימות מזהה וסיסמה, וקביעת המשתמש המחובר.
        // מחזירה את המשתמש אם ההתחברות הצליחה, או null אם נכשלה -
        // בלי לפרט אם הבעיה הייתה במזהה או בסיסמה, כדי לא לחשוף מידע.
        public User? Login(string id, string password)
        {
            User? user = FindUserById(id);
            if (user == null)
                return null;
            if (!user.CheckPassword(password))
                return null;
            if (!user.IsActive())
                return null;

            currentUser = user;
            return user;
        }
    }
}