# 🏋️ GymSystem
 
מערכת לניהול חדר כושר — פרויקט קבוצתי (Apex Sharks).
 
19 דרישות (REQ-001 עד REQ-019), כל אחת עם Story ב-Jira ולעיתים Subtasks. תהליך: Jira (מה) → Confluence (איך) → Branch → קוד → Commit → PR → Review → Merge.
 
## 🔗 קישורים חיים
 
| מה | קישור | שימוש |
|---|---|---|
| 📋 Jira board | [כל ה-REQ-ים, ממוינים](https://nuriel-altstein.atlassian.net/jira/software/projects/KAN/list?jql=project%20%3D%20KAN%20ORDER%20BY%20cf%5B10019%5D%20ASC) | מצב עדכני של כל משימה |
| 📚 Confluence space | [Apex Sharks](https://nuriel-altstein.atlassian.net/wiki/spaces/AS/folder/1212417/Apex+Sharks) | כל מסמכי התכנון (ראה למטה) |
 
מתוך ה-space, הדפים הרלוונטיים ביותר לפיתוח שוטף:
- **מסמך דרישות ראשי** — פירוט כל REQ
- **מסמך עיצוב טכני - גרסה מורחבת** — Classes / Attributes / Methods / קשרים (הגרסה הפעילה שעובדים לפיה)
- **החלטות סופיות לפני תחילת הקידוד**
> 📌 ה-README לא משכפל תוכן מ-Jira/Confluence. רשימת הדרישות והעיצוב חיים שם ומתעדכנים שם — קישור, לא העתק.
 
## 🌿 מבנה Branches
 
```
master                        ← גרסה יציבה, לא עובדים עליו ישירות
└── GymSystem                 ← branch פיתוח מרכזי
    ├── netanel/appointments
    ├── netanel/class-registration
    ├── netanel/classes-scheduling
    ├── nuriel/identity
    ├── nuriel/staff-management
    ├── yossi/progress-tracking
    ├── yossi/trainee-membership
    └── yossi/workout-nutrition
```
 
## 🔄 תהליך עבודה למשימה חדשה
 
1. **Jira** — מצא את ה-REQ שלך, קרא את הדרישה וה-Subtasks
2. **Confluence** — בדוק את מסמך העיצוב הטכני (Classes / Methods / Dependencies) למשימה הזו
3. **Branch** — `git checkout GymSystem` ואז `git checkout <your-branch>`
4. **קוד** — רק על החלק של המשימה שלך
5. **בדיקה** — מתקמפל, עובד, לא שבר משהו אחר
6. **Commit + Push** — הודעה ברורה (`git commit -m "Implement appointment management"`)
7. **Pull Request → Review → Merge** — לא Merge עצמי בלי Review כשנדרש
## 📌 כללי עבודה
 
- לא עובדים ישירות על `master`
- לא נוגעים בקוד של אחר בלי תיאום
- Commit מסביר מה השתנה, לא רק "fix"
- לא Push בלי לבדוק שהקוד עובד
- שאלה שלא ברורה → שואלים לפני שממשיכים
## 👥 צוות
 
| חבר צוות | תחום |
|---|---|
| נתנאל בבייב | Appointments, Class Registration, Classes Scheduling |
| נוריאל אלטשטיין | Identity, Staff Management |
| יוסי | Progress Tracking, Trainee Membership, Workout Nutrition |
 
---
**GymSystem — בונים את זה ביחד, REQ אחרי REQ.**
 