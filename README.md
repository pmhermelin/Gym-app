<div dir="rtl">

<div align="right">בס"ד</div>

</div>

# 🏋️ GymSystem

> **מערכת לניהול חדר כושר**
> פרויקט קבוצתי — **Apex Sharks**

מערכת לניהול ותפעול חדר כושר, המפותחת כפרויקט קבוצתי תוך עבודה מסודרת עם **Jira, Confluence ו־GitHub**.

---

## 🎯 על הפרויקט

המערכת כוללת **19 דרישות פונקציונליות**, החל מ־`REQ-001` ועד `REQ-019`.

כל דרישה מיוצגת באמצעות **Story ב־Jira**, ובמידת הצורך מחולקת ל־**Subtasks** לצורך פירוק העבודה.

הדרישות והעיצוב מתועדים ב־Confluence, המשימות מנוהלות ב־Jira, והקוד מנוהל באמצעות GitHub.

---

## 🧩 כלי העבודה

| כלי                 | תפקיד                                     |
| ------------------- | ----------------------------------------- |
| 📚 **Confluence**   | דרישות המערכת, מסמכי עיצוב ותכנון         |
| 📋 **Jira**         | ניהול Stories, Subtasks ומעקב אחר התקדמות |
| 💻 **GitHub**       | ניהול קוד המקור ו־Branches                |
| 🔀 **Pull Request** | הגשת קוד לבדיקה לפני שילוב ב־`master`     |

---

# 🔄 תהליך העבודה

<div dir="ltr">

```text
Jira
  ↓
Confluence
  ↓
Branch
  ↓
Code
  ↓
Commit
  ↓
Push
  ↓
Pull Request
  ↓
Code Review
  ↓
Merge → master
  ↓
Jira → Done
```

</div>

### מה המשמעות של כל שלב?

* **Jira** — מגדיר מה צריך לפתח.
* **Confluence** — מגדיר את הדרישות, העיצוב והתכנון.
* **Branch** — סביבת העבודה של חבר הצוות.
* **Code** — מימוש ה־Story.
* **Commit** — שמירת השינויים וקישורם ל־Jira.
* **Push** — העלאת העבודה ל־GitHub.
* **Pull Request** — הגשת העבודה לבדיקה.
* **Code Review** — בדיקת הקוד.
* **Merge** — שילוב הקוד ב־`master`.
* **Jira → Done** — סימון ה־Story כהושלם.

---

# 🔗 קישורים חשובים

| 🔎 משאב                                                                                                                                           | 📌 שימוש                       |
| ------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------ |
| [📋 Jira Board](https://nuriel-altstein.atlassian.net/jira/software/projects/KAN/list?jql=project%20%3D%20KAN%20ORDER%20BY%20cf%5B10019%5D%20ASC) | ניהול ומעקב אחר דרישות ומשימות |
| [📚 Confluence — Apex Sharks](https://nuriel-altstein.atlassian.net/wiki/spaces/AS/folder/1212417/Apex+Sharks)                                    | דרישות, עיצוב ותכנון הפרויקט   |

### 📚 מסמכים מרכזיים

ב־Confluence נמצאים מסמכי הפרויקט המרכזיים:

* **מסמך דרישות** — דרישות המערכת `REQ-001` עד `REQ-019`
* **מסמך עיצוב טכני** — Classes, Attributes, Methods וקשרים בין הרכיבים
* **מסמכי תכנון והחלטות** — החלטות שהתקבלו לפני תחילת המימוש

> 💡 **ה־README משמש כמפת ניווט לפרויקט.**
> המידע המלא והמעודכן נמצא ב־Jira וב־Confluence.

---

# 👥 צוות הפרויקט

**Apex Sharks** כולל שלושה חברי צוות:

* 👨‍💻 **נתנאל בבייב**
* 👨‍💻 **נוריאל אלטשטיין**
* 👨‍💻 **Yossi Gerafi**

כל **Story מוקצה לחבר צוות אחד בלבד**.

החבר שאליו הוקצה ה־Story אחראי על **ה־Story במלואו**, כולל ה־Subtasks הקשורים אליו.

> ⚠️ אין לחלק Story בין שני חברי צוות.

---

# 🌿 מבנה ה־Branches

המרצה אישר לעבוד עם **8 Branches לפי תחומי אחריות**, במקום ליצור Branch נפרד לכל אחת מ־19 הדרישות.

<div dir="ltr">

```text
master
│
├── netanel/appointments
├── netanel/class-registration
├── netanel/classes-scheduling
│
├── nuriel/identity
├── nuriel/staff-management
│
├── yossi/progress-tracking
├── yossi/trainee-membership
└── yossi/workout-nutrition
```

</div>

### 👤 חלוקת ה־Branches

| חבר צוות            | Branches                                                                               |
| ------------------- | -------------------------------------------------------------------------------------- |
| **נתנאל בבייב**     | `netanel/appointments`<br>`netanel/class-registration`<br>`netanel/classes-scheduling` |
| **נוריאל אלטשטיין** | `nuriel/identity`<br>`nuriel/staff-management`                                         |
| **Yossi Gerafi**    | `yossi/progress-tracking`<br>`yossi/trainee-membership`<br>`yossi/workout-nutrition`   |

### 📌 חשוב

`master` הוא הענף המרכזי של הפרויקט.

**אין לבצע עבודה ישירות על `master`.**

ה־Branches מחולקים לפי **תחומי אחריות**, ולכן Branch אחד יכול להכיל מספר Stories של אותו חבר צוות.

---
# 📋 חלוקת ה־Stories

להלן המיפוי של **19 ה־Stories ב־Jira**, כולל KAN, REQ, Branch והאחראי.

| חבר צוות            | Branch                       | KAN      | REQ       | Story                        |
| ------------------- | ---------------------------- | -------- | --------- | ---------------------------- |
| **נוריאל אלטשטיין** | `nuriel/identity`            | `KAN-8`  | `REQ-001` | יצירת חשבון                  |
|                     |                              | `KAN-9`  | `REQ-002` | התחברות                      |
|                     |                              | `KAN-13` | `REQ-006` | חיפוש משתמשים                |
|                     | `nuriel/staff-management`    | `KAN-10` | `REQ-003` | דוח הכנסות                   |
|                     |                              | `KAN-11` | `REQ-004` | ניהול עובדים                 |
| **Yossi Gerafi**    | `yossi/trainee-membership`   | `KAN-12` | `REQ-005` | הוספת מתאמן וניהול מנוי      |
|                     |                              | `KAN-16` | `REQ-009` | חיפוש מתאמנים משויכים        |
|                     | `yossi/progress-tracking`    | `KAN-17` | `REQ-010` | תיעוד ועדכון התקדמות         |
|                     |                              | `KAN-26` | `REQ-019` | צפייה בהתקדמות אישית         |
|                     | `yossi/workout-nutrition`    | `KAN-18` | `REQ-011` | ניהול תוכנית אימונים         |
|                     |                              | `KAN-19` | `REQ-012` | ניהול תפריט תזונה            |
|                     |                              | `KAN-22` | `REQ-015` | צפייה בתוכנית ובתפריט אישיים |
| **נתנאל בבייב**     | `netanel/classes-scheduling` | `KAN-14` | `REQ-007` | הוספת שיעור                  |
|                     |                              | `KAN-15` | `REQ-008` | עדכון/ביטול שיעור            |
|                     | `netanel/appointments`       | `KAN-20` | `REQ-013` | קביעת פגישה                  |
|                     |                              | `KAN-21` | `REQ-014` | עדכון/ביטול פגישה            |
|                     | `netanel/class-registration` | `KAN-23` | `REQ-016` | חיפוש שיעורים                |
|                     |                              | `KAN-24` | `REQ-017` | הרשמה לשיעור                 |
|                     |                              | `KAN-25` | `REQ-018` | לוח אישי וביטול הרשמה        |

### 📊 סיכום החלוקה הנוכחית

| חבר צוות        | מספר Stories |
| --------------- | -----------: |
| נוריאל אלטשטיין |        **5** |
| Yossi Gerafi    |        **7** |
| נתנאל בבייב     |        **7** |
| **סה״כ**        |       **19** |

> ⚠️ החלוקה הנוכחית היא `5 / 7 / 7`.
> לפני שתוצג כחלוקה סופית, יש לאזן אותה בהתאם לדרישת החלוקה ההוגנת של הפרויקט, שבה ההפרש בין חברי הצוות הוא לכל היותר Story אחד.

---

# 🌿 Branch לפי תחום — Story לפי משימה

ה־Branch מייצג **תחום עבודה**, ואילו ה־Story מייצג **יחידת פיתוח מלאה**.

לכן Branch אחד יכול להכיל מספר Stories.

לדוגמה:

<div dir="ltr">

```text
netanel/classes-scheduling
│
├── KAN-14 → REQ-007 → הוספת שיעור
└── KAN-15 → REQ-008 → עדכון/ביטול שיעור
```

</div>

במקרה כזה:

* `netanel/classes-scheduling` — סביבת העבודה של התחום.
* `KAN-14` — Story אחד.
* `KAN-15` — Story אחר.
* כל Story שייך לנתנאל בלבד.
* אין לחלק את אותו Story בין חברי צוות.

---

# 🔧 תהליך עבודה עם Git

## 1️⃣ עדכון `master`

לפני תחילת עבודה:

<div dir="ltr">

```bash
git switch master
git pull
```

</div>

מומלץ להתחיל עבודה כאשר `master` המקומי מעודכן.

---

## 2️⃣ מעבר ל־Branch שלך

לדוגמה:

<div dir="ltr">

```bash
git switch netanel/class-registration
```

</div>

אם ה־Branch קיים ב־GitHub אך עדיין לא קיים אצלך מקומית:

<div dir="ltr">

```bash
git switch --track origin/netanel/class-registration
```

</div>

---

## 3️⃣ מימוש ה־Story

עובדים על ה־Story שהוקצה לך בהתאם ל־Jira ול־Confluence.

> ⚠️ אין לבצע עבודה ישירות על `master`.

---

## 4️⃣ בדיקת השינויים

<div dir="ltr">

```bash
git status
```

</div>

---

## 5️⃣ הוספת השינויים

להוספת כל השינויים:

<div dir="ltr">

```bash
git add .
```

</div>

או קובץ מסוים:

<div dir="ltr">

```bash
git add filename
```

</div>

---

## 6️⃣ Commit

ה־Commit צריך לכלול את **מפתח ה־Jira של ה־Story**.

לדוגמה:

<div dir="ltr">

```bash
git commit -m "KAN-14 Implement AddClass"
```

</div>

או:

<div dir="ltr">

```bash
git commit -m "KAN-15 Implement UpdateClass and CancelClass"
```

</div>

> 📌 חשוב להשתמש במפתח ה־KAN האמיתי של ה־Story שעליו עובדים.

---

## 7️⃣ Push

<div dir="ltr">

```bash
git push
```

</div>

---

## 8️⃣ Pull Request

לאחר השלמת ה־Story:

<div dir="ltr">

```text
Your Branch
     ↓
Pull Request
     ↓
master
```

</div>

פותחים **Pull Request מה־Branch אל `master`**.

לאחר מכן:

<div dir="ltr">

```text
Pull Request
     ↓
Code Review
     ↓
Approval
     ↓
Merge → master
```

</div>

---

## 9️⃣ עדכון Jira

לאחר שה־Story הושלם, נבדק ואוחד ל־`master`:

<div dir="ltr">

```text
Story → Done
```

</div>

---

# 🧹 Gitignore

קובץ `.gitignore` צריך להיות חלק מה־Repository.

מטרתו למנוע העלאה של קבצים ותיקיות שאינם צריכים להיות מנוהלים ב־Git, כגון קבצים שנוצרים אוטומטית על ידי סביבת הפיתוח או במהלך Build.

יש לוודא שה־`.gitignore` מוגדר **לפני** הוספת הקבצים ל־Git.

---

# ✅ כללי עבודה

### כן ✔️

* לעבוד על ה־Branch שהוקצה לך.
* לעבוד לפי Jira ו־Confluence.
* להשלים את ה־Story במלואו.
* לקשר את ה־Commit ל־Story באמצעות מפתח ה־KAN.
* לבצע `push` ל־GitHub.
* לפתוח Pull Request אל `master`.
* לבצע Code Review.
* לבצע Merge לאחר הבדיקה.
* לעדכן את ה־Story ל־`Done`.

### לא ❌

* לא לעבוד ישירות על `master`.
* לא לחלק Story בין חברי צוות.
* לא לעבוד על Story שהוקצה לחבר אחר.
* לא לבצע Merge ללא Review.
* לא להכניס קבצים מיותרים ל־Repository.
* לא לשנות את הדרישות או העיצוב ללא החלטה מתאימה.

---

# ⭐ מה הכי חשוב לזכור?

<div dir="ltr">

```text
Jira
  ↓
What?
  ↓
Confluence
  ↓
How?
  ↓
Your Branch
  ↓
Complete Story
  ↓
Commit + KAN
  ↓
Push
  ↓
Pull Request → master
  ↓
Code Review
  ↓
Merge
  ↓
Jira → Done
```

</div>

---

<div align="center">

# 🦈 Apex Sharks

**REQ אחרי REQ · Story אחרי Story · Commit אחרי Commit**

### 🚀 בונים את GymSystem ביחד.

</div>
