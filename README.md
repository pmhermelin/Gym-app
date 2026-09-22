<div dir="rtl">

<div align="right">בס"ד</div>

</div>

# 🏋️ GymSystem

> **מערכת לניהול חדר כושר**
> פרויקט קבוצתי — **Apex Sharks**

מערכת לניהול ותפעול חדר כושר, המפותחת כפרויקט קבוצתי תוך שימוש ב־**Jira, Confluence ו־GitHub** ובהתאם לתהליך העבודה שהוגדר לפרויקט.

---

## 🎯 על הפרויקט

המערכת כוללת **19 דרישות פונקציונליות**, החל מ־`REQ-001` ועד `REQ-019`.

הדרישות מתועדות ב־**Confluence**, מנוהלות באמצעות **Jira**, וממומשות בקוד באמצעות **GitHub**.

### 🧩 הכלים שבהם אנו משתמשים

| כלי                 | תפקיד                                     |
| ------------------- | ----------------------------------------- |
| 📚 **Confluence**   | דרישות המערכת, מסמכי עיצוב ותכנון         |
| 📋 **Jira**         | ניהול Stories, Subtasks ומעקב אחר התקדמות |
| 💻 **GitHub**       | ניהול קוד המקור ועבודה עם Branches        |
| 🔀 **Pull Request** | הגשת קוד לבדיקה לפני שילוב ב־`master`     |

---

## 🔄 תהליך העבודה

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

* **Jira** — מגדיר *מה* צריך לפתח.
* **Confluence** — מגדיר את הדרישות, העיצוב והתכנון.
* **Branch** — סביבת העבודה של חבר הצוות.
* **Code** — מימוש הדרישה.
* **Commit** — שמירת שינוי ברור ומקושר למשימה.
* **Push** — העלאת העבודה ל־GitHub.
* **Pull Request** — בקשה לשלב את העבודה ב־`master`.
* **Code Review** — בדיקת הקוד.
* **Merge** — שילוב הקוד בפרויקט.
* **Jira → Done** — סגירת ה־Story לאחר השלמת העבודה.

---

## 🔗 קישורים חשובים

| 🔎 משאב                                                                                                                                           | 📌 שימוש                       |
| ------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------ |
| [📋 Jira Board](https://nuriel-altstein.atlassian.net/jira/software/projects/KAN/list?jql=project%20%3D%20KAN%20ORDER%20BY%20cf%5B10019%5D%20ASC) | ניהול ומעקב אחר דרישות ומשימות |
| [📚 Confluence — Apex Sharks](https://nuriel-altstein.atlassian.net/wiki/spaces/AS/folder/1212417/Apex+Sharks)                                    | דרישות, עיצוב ותכנון הפרויקט   |

### 📚 מסמכי הפרויקט

המסמכים המרכזיים נמצאים ב־Confluence:

* **מסמך דרישות** — דרישות המערכת `REQ-001` עד `REQ-019`
* **מסמך עיצוב טכני** — Classes, Attributes, Methods וקשרים בין הרכיבים
* **מסמכי תכנון והחלטות** — החלטות שהתקבלו לפני תחילת המימוש

> 💡 **ה־README הוא מפת הניווט של הפרויקט.**
> המידע המלא והמעודכן נמצא ב־Jira וב־Confluence.

---

# 👥 חלוקת העבודה

הפרויקט מפותח על ידי שלושה חברי צוות.

המרצה אישר לעבוד עם **8 Branches לפי תחומי אחריות**, במקום ליצור Branch נפרד עבור כל אחת מ־19 הדרישות.

### 🌿 Branches

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

### 👤 תחומי אחריות

| חבר צוות            | Branches                                                         |
| ------------------- | ---------------------------------------------------------------- |
| **נתנאל בבייב**     | `appointments` · `class-registration` · `classes-scheduling`     |
| **נוריאל אלטשטיין** | `identity` · `staff-management`                                  |
| **Yossi Gerafi**    | `progress-tracking` · `trainee-membership` · `workout-nutrition` |

> ⚠️ **`master` הוא הענף המרכזי של הפרויקט.**
> אין לבצע עבודה ישירות על `master`.

---

## 📋 חלוקת ה־REQs

הפרויקט כולל 19 Stories, כאשר כל Story משויך לחבר צוות אחד.

> ⚠️ החלוקה הסופית חייבת לעמוד בדרישת המרצה של חלוקה הוגנת — **הפרש של לכל היותר Story אחד בין חברי הצוות**.

| חבר צוות            | Stories | REQs                                                                        |
| ------------------- | ------: | --------------------------------------------------------------------------- |
| **נוריאל אלטשטיין** |      5* | `REQ-001`, `REQ-002`, `REQ-003`, `REQ-004`, `REQ-006`                       |
| **Yossi Gerafi**    |      7* | `REQ-005`, `REQ-009`, `REQ-010`, `REQ-011`, `REQ-012`, `REQ-015`, `REQ-019` |
| **נתנאל בבייב**     |      7* | `REQ-007`, `REQ-008`, `REQ-013`, `REQ-014`, `REQ-016`, `REQ-017`, `REQ-018` |

* **יש לאזן את החלוקה לפני שמציגים אותה כחלוקה סופית**, מכיוון שהחלוקה הנוכחית היא `5 / 7 / 7`.

---

# 🌿 איך עובדים עם ה־Branches?

ה־Branches שלנו מחולקים לפי **תחומי אחריות**, ולא לפי כל REQ בנפרד.

לכן:

> **Branch אחד יכול להכיל מספר REQs.**

לדוגמה:

<div dir="ltr">

```text
netanel/classes-scheduling
│
├── REQ-007
└── REQ-008
```

</div>

המשמעות היא שה־Branch מייצג את תחום הפיתוח, בעוד שכל שינוי בקוד צריך להיות מקושר ל־Story המתאים ב־Jira.

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

כך מתחילים מהגרסה העדכנית של הפרויקט.

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

## 3️⃣ עובדים על ה־REQ שהוקצה

מבצעים את המימוש בהתאם ל־**Jira + Confluence**.

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

<div dir="ltr">

```bash
git add .
```

</div>

או עבור קובץ מסוים:

<div dir="ltr">

```bash
git add filename
```

</div>

---

## 6️⃣ Commit

ה־Commit צריך להיות ברור ולכלול את **מפתח ה־Jira של ה־Story**.

לדוגמה:

<div dir="ltr">

```bash
git commit -m "KAN-XX Implement class registration"
```

</div>

> 📌 מחליפים את `KAN-XX` במפתח ה־Jira האמיתי של ה־Story.

---

## 7️⃣ Push

<div dir="ltr">

```bash
git push
```

</div>

---

## 8️⃣ Pull Request

לאחר השלמת העבודה:

<div dir="ltr">

```text
Your Branch
     ↓
Pull Request
     ↓
master
```

</div>

פותחים **Pull Request אל `master`**.

לאחר מכן:

```text
Pull Request
     ↓
Code Review
     ↓
Approval
     ↓
Merge
```

---

## 9️⃣ עדכון Jira

לאחר שה־Story הושלם, נבדק ואוחד ל־`master`:

```text
Story → Done
```

---

# 📝 כללי עבודה חשובים

### ✅ כן

* לעבוד רק על ה־Branch שהוקצה לך.
* לעבוד בהתאם ל־Jira ול־Confluence.
* לקשר Commits ל־Jira באמצעות מפתח ה־Story.
* לבצע `push` ל־GitHub.
* לפתוח Pull Request אל `master`.
* לבצע Code Review לפני Merge.
* לעדכן את ה־Story ב־Jira לאחר השלמת העבודה.

### ❌ לא

* לא לעבוד ישירות על `master`.
* לא לבצע Merge ללא Review.
* לא לבצע עבודה על Story שמוקצה לחבר אחר.
* לא להמציא דרישות או לשנות את העיצוב ללא החלטה מתאימה.
* לא להכניס קבצים מיותרים או קבצי מערכת ל־Repository.

---

# 🧹 Gitignore

יש לוודא שקובץ `.gitignore` קיים בפרויקט לפני הוספת הקבצים ל־Git.

אין להעלות ל־Repository קבצים ותיקיות שאינם חלק מקוד המקור, כגון קבצי IDE וקבצים שנוצרים אוטומטית במהלך הבנייה.

---

# ⭐ מה לזכור?

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
Code
  ↓
Commit + Jira Key
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

**REQ אחרי REQ · Branch אחרי Branch · Commit אחרי Commit**

### 🚀 בונים את GymSystem ביחד.

</div>
