# 🏋️ GymSystem

> **מערכת לניהול חדר כושר**
> פרויקט קבוצתי — **Apex Sharks**

מערכת לניהול ותפעול חדר כושר, המפותחת כפרויקט קבוצתי תוך עבודה מסודרת לפי תהליך פיתוח מקצועי.

---

## 🎯 על הפרויקט

הפרויקט כולל **19 דרישות מערכת**, החל מ־`REQ-001` ועד `REQ-019`.

לכל דרישה קיימת **Story ב־Jira**, ובמקרים הרלוונטיים גם **Subtasks** המגדירים את משימות הפיתוח.

### 🔄 תהליך העבודה

<div dir="ltr">

```text
Jira → Confluence → Branch → Code → Commit → Pull Request → Review → Merge
```

</div>

**Jira** — מה המערכת צריכה לעשות
**Confluence** — איך המערכת מתוכננת
**Branch** — סביבת העבודה של המשימה
**Code** — מימוש הדרישה
**Pull Request** — הגשת השינוי לבדיקה
**Review** — בדיקת הקוד
**Merge** — שילוב הקוד בפרויקט

---

## 🔗 קישורים חשובים

| 🔎 משאב                                                                                                                                           | 📌 שימוש                               |
| ------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------- |
| [📋 Jira Board](https://nuriel-altstein.atlassian.net/jira/software/projects/KAN/list?jql=project%20%3D%20KAN%20ORDER%20BY%20cf%5B10019%5D%20ASC) | מצב עדכני של כל דרישות המערכת והמשימות |
| [📚 Confluence — Apex Sharks](https://nuriel-altstein.atlassian.net/wiki/spaces/AS/folder/1212417/Apex+Sharks)                                    | מסמכי הדרישות, העיצוב והתכנון          |

### 📚 מסמכים חשובים ב־Confluence

מתוך ה־Space של **Apex Sharks**, המסמכים המרכזיים לעבודה השוטפת הם:

* **מסמך דרישות ראשי** — פירוט דרישות המערכת `REQ-001` עד `REQ-019`
* **מסמך עיצוב טכני — גרסה מורחבת** — Classes, Attributes, Methods וקשרים בין הרכיבים
* **החלטות סופיות לפני תחילת הקידוד** — החלטות שהתקבלו לפני המימוש

> 💡 **עיקרון חשוב:**
> ה־README משמש כמפת ניווט לפרויקט.
> התוכן המלא של הדרישות והעיצוב נמצא ומתעדכן ב־Jira וב־Confluence.

---

## 🌿 מבנה ה־Branches

<div dir="ltr">

```text
master
│
└── GymSystem
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

### 📌 תפקיד ה־Branches

* `master` — גרסה יציבה של הפרויקט
* `GymSystem` — ענף הפיתוח המרכזי
* `feature branches` — עבודה על דרישות ופיצ'רים ספציפיים

> ⚠️ **אין לבצע עבודה ישירות על `master`.**

---

## 🔄 איך עובדים על דרישה?

### 1️⃣ Jira

מצא את ה־`REQ` שלך וקרא:

* את ה־Story
* את הדרישה המלאה
* את ה־Subtasks
* את ה־Dependencies הרלוונטיים

### 2️⃣ Confluence

בדוק את **מסמך העיצוב הטכני**:

* Classes
* Attributes
* Methods
* קשרים בין מחלקות
* Dependencies

### 3️⃣ מעבר ל־Branch

<div dir="ltr">

```bash
git checkout GymSystem
git checkout <your-branch>
```

</div>

### 4️⃣ כתיבת הקוד

עבוד רק על החלק שהוקצה לך.

לפני הוספת שינוי שמשפיע על חלק אחר במערכת — **מתאמים עם חבר הצוות הרלוונטי**.

### 5️⃣ בדיקה

לפני Commit:

* הקוד מתקמפל
* הדרישה עובדת
* אין שגיאות חדשות
* לא נשבר קוד קיים

### 6️⃣ Commit + Push

השתמש בהודעת Commit ברורה שמתארת את השינוי.

<div dir="ltr">

```bash
git add .
git commit -m "Implement appointment management"
git push
```

</div>

### 7️⃣ Pull Request

לאחר סיום העבודה:

<div dir="ltr">

```text
Branch → Pull Request → Code Review → Merge
```

</div>

אין לבצע **Merge עצמי** כאשר נדרש Review.

---

## 📋 כללי עבודה

| ✅ כלל           | 📌 משמעות                                          |
| --------------- | -------------------------------------------------- |
| 🚫 `master`     | לא עובדים ישירות על `master`                       |
| 🤝 עבודה משותפת | לא משנים קוד של חבר צוות ללא תיאום                 |
| 📝 Commits      | כל Commit מתאר בצורה ברורה את השינוי               |
| 🧪 בדיקות       | לא מבצעים Push לפני שבודקים שהקוד עובד             |
| 🔍 Review       | שינויים עוברים בדיקה לפני Merge בהתאם לתהליך הצוות |
| ❓ חוסר ודאות    | אם משהו לא ברור — עוצרים ושואלים לפני שממשיכים     |

---

## 👥 חלוקת עבודה

| 👤 חבר צוות         | 💻 תחומי אחריות                                            |
| ------------------- | ---------------------------------------------------------- |
| **נתנאל בבייב**     | Appointments · Class Registration · Classes Scheduling     |
| **נוריאל אלטשטיין** | Identity · Staff Management                                |
| **יוסי**            | Progress Tracking · Trainee Membership · Workout Nutrition |

---

## 🧭 עקרונות הפרויקט

**Jira מגדיר מה בונים.**
**Confluence מגדיר איך מתכננים.**
**Git מנהל את הקוד.**
**Pull Request מאפשר Review.**
**הצוות בונה את המערכת ביחד.**

---

<div align="center">

### 🦈 Apex Sharks

**REQ אחרי REQ · Branch אחרי Branch · Commit אחרי Commit**

> 🚀 **בונים את GymSystem ביחד.**

</div>
