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
## 🔄 תהליך עבודה עם Git

העבודה על הקוד מתבצעת לפי **תחומים**, ולא לפי REQ בודד.

כל חבר צוות עובד על ה־Branches שהוקצו לו, ו־Branch אחד יכול להכיל מספר REQs.

### 1️⃣ לפני שמתחילים

מעבר ל־`master` ועדכון הגרסה המקומית:

<div dir="ltr">

```bash
git switch master
git pull
```

> 💡 מומלץ להתחיל עבודה תמיד מ־`master` מעודכן.

### 2️⃣ עוברים ל־Branch שלך

לדוגמה, אם הענף שלך הוא:

<div dir="ltr">

```bash
git switch yossi/progress-tracking
```

אם ה־Branch קיים ב־GitHub אבל עדיין לא קיים אצלך במחשב:

<div dir="ltr">

```bash
git switch --track origin/yossi/progress-tracking
```

אם מדובר ב־Branch חדש שעדיין לא קיים ב־GitHub:

<div dir="ltr">

```bash
git switch -c yossi/progress-tracking
git push -u origin yossi/progress-tracking
```

### 3️⃣ עובדים רק על ה־Branch שלך

מבצעים את הקוד של ה־REQ שהוקצה לך.

> ⚠️ לא עובדים ישירות על `master`.

### 4️⃣ בודקים מה השתנה

בסיום העבודה או לפני Commit:

<div dir="ltr">

```bash
git status
```

פקודה זו מציגה אילו קבצים השתנו.

### 5️⃣ מוסיפים את השינויים

אם רוצים להוסיף את כל השינויים:

<div dir="ltr">

```bash
git add .
```

או קובץ ספציפי:

<div dir="ltr">

```bash
git add filename
```

### 6️⃣ מבצעים Commit

בכל Commit מציינים את ה־`KAN` של ה־REQ שעליו עובדים.

לדוגמה:

<div dir="ltr">

```bash
git commit -m "KAN-17 Add progress record"
```

כך ניתן לקשר את ה־Commit למשימה המתאימה ב־Jira.

> 📌 **חשוב:** ה־Branch הוא לפי תחום, אבל ה־`KAN` הוא לפי המשימה הספציפית שעליה עובדים.

### 7️⃣ מעלים ל־GitHub

<div dir="ltr">

```bash
git push
```

### 8️⃣ מסיימים את ה־REQ

לאחר שה־REQ הושלם:

<div dir="ltr">

```text
Your Branch
     ↓
Pull Request
     ↓
master
```

פותחים **Pull Request** מה־Branch שלך אל `master`.

לאחר מכן מתבצע **Code Review**, ורק לאחר שהכול תקין מבצעים **Merge**.

---

## ⭐ מה הכי חשוב לזכור?

<div dir="ltr">

```text
Branch
  ↓
Code
  ↓
git add
  ↓
Commit + KAN
  ↓
Push
  ↓
Pull Request
  ↓
Code Review
  ↓
Merge → master
```

---

## 🌿 Branch לפי תחום, Commit לפי REQ

החלוקה שלנו היא לפי **תחומי אחריות**, ולכן Branch אחד יכול להכיל מספר REQs.

לדוגמה:

<div dir="ltr">

```text
netanel/classes-scheduling
│
├── KAN-14 / REQ-007
└── KAN-15 / REQ-008
```

</div>

במקרה כזה:

* `netanel/classes-scheduling` — ה־Branch של התחום
* `KAN-14` — המשימה שעליה עובדים עכשיו
* `REQ-007` — הדרישה המתאימה

לכן כל Commit צריך לציין את ה־`KAN` הספציפי של השינוי.

לדוגמה:

<div dir="ltr">

```bash
git commit -m "KAN-14 Implement class scheduling"
```

ובהמשך:

<div dir="ltr">

```bash
git commit -m "KAN-15 Add class availability"
```

> 🦈 **Branch אחד יכול להכיל כמה REQs — אבל כל Commit צריך להיות ברור ולציין לאיזו משימת Jira הוא שייך.**


### 🦈 Apex Sharks

**REQ אחרי REQ · Branch אחרי Branch · Commit אחרי Commit**

> 🚀 **בונים את GymSystem ביחד.**

</div>
