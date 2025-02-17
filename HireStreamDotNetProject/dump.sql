-- PRAGMA foreign_keys=OFF;

create database HireStreamProduction;
use HireStreamProduction;
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsLock" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK___EFMigrationsLock" PRIMARY KEY,
    "Timestamp" TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);
INSERT INTO __EFMigrationsHistory VALUES('20250123190425_InitialCreateUserModel','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250123225421_UpdatedUserModel','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250125101527_UpdatedUserModelWithRegisterDateField','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250125104548_AddedPasswordHashFieldToUserModel','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250125121712_CreatedBaseModelForContactForm','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250125121948_AddedPKToContactsModel','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250126104115_UpdatedUserModelForProfileAttrs','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250126113940_AddedUserRoleFieldInUserModel','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250128215829_UpdatedUsersModelForBetterDataHandling','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250131165306_AddedJobPostModel','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250131171623_AddedJobPostModelToContext','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250131171859_AddedApplicationModelToTheProjectAndRegisteredItToContext','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250201192039_AddedJobCategoryModel','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250201192347_UpdatedJobPostModel','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250201193232_StillFixingMyStupidity','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250204001229_AddedResetPasswordTable','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250204130440_AddedNewsLetterModelToTheProject','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250204130552_RegisteredNewsletterModel','9.0.1');
INSERT INTO __EFMigrationsHistory VALUES('20250204134238_UpdatedUserModelForOptionalFields','9.0.1');
CREATE TABLE IF NOT EXISTS "Contacts" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Contacts" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Message" TEXT NOT NULL,
    "SubmitTime" TEXT NOT NULL,
    "IsSent" INTEGER NOT NULL
);
INSERT INTO Contacts VALUES(1,'usman shahid','usmanshahid027@outlook.com','testing message, lets see what happens','2025-01-30 11:06:54.6954173',0);
INSERT INTO Contacts VALUES(2,'usman shahid','usmanshahid027@outlook.com','testing message, lets see what happens','2025-01-30 11:10:58.9356215',0);
INSERT INTO Contacts VALUES(3,'usman shahid','usmanshahid027@outlook.com','testing message, lets see what happens','2025-01-30 11:16:00.3857114',0);
INSERT INTO Contacts VALUES(4,'usman shahid','usmanshahid027@outlook.com','testing message','2025-01-31 00:09:21.6150387',1);
INSERT INTO Contacts VALUES(5,'usman','muhammadusman27virgo@yahoo.com','testing again for messaging!','2025-01-31 00:22:43.1443076',1);
INSERT INTO Contacts VALUES(6,'usman shahid','l1f22bscs1057@ucp.edu.pk','testing again for messaging!','2025-01-31 00:25:50.2734701',1);
INSERT INTO Contacts VALUES(7,'usman shahid','l1f22bscs1057@ucp.edu.pk','testing again for messaging!','2025-01-31 00:25:52.2812785',1);
INSERT INTO Contacts VALUES(8,'nothing new','usmanshahid027@outlook.com','still testing email service','2025-01-31 00:32:30.9742328',1);
INSERT INTO Contacts VALUES(9,'nothing new','usmanshahid027@outlook.com','still testing email service','2025-01-31 00:32:59.6876552',1);
INSERT INTO Contacts VALUES(10,'usman shahid','muhammadusman27virgo@yahoo.com','testing message from smtp client','2025-01-31 00:45:20.9851979',1);
INSERT INTO Contacts VALUES(11,'usman shahid','muhammadusman27virgo@yahoo.com','testing message from smtp client','2025-01-31 01:58:11.174849',1);
INSERT INTO Contacts VALUES(12,'usman shahid','muhammadusman27virgo@yahoo.com','testing message from smtp client','2025-01-31 02:00:55.5431787',1);
INSERT INTO Contacts VALUES(13,'usman shahid','muhammadusman27virgo@yahoo.com','testing message from smtp client','2025-01-31 02:07:38.6510438',1);
INSERT INTO Contacts VALUES(14,'usman shahid','muhammadusman27virgo@yahoo.com','testing message from smtp client','2025-01-31 02:09:56.6061792',1);
INSERT INTO Contacts VALUES(15,'usman shahid','muhammadusman27virgo@yahoo.com','testing message from smtp client','2025-01-31 02:13:28.60978',1);
INSERT INTO Contacts VALUES(16,'usman shahid','muhammadusman27virgo@yahoo.com','testing message from smtp client','2025-01-31 02:23:30.8153991',1);
INSERT INTO Contacts VALUES(17,'Usman Shahid','usmanshahid027@outlook.com','testing one last time for email configurations','2025-01-31 02:30:50.807414',1);
INSERT INTO Contacts VALUES(18,'Usman Shahid','usmanshahid027@outlook.com','just testing with new subject change!','2025-01-31 02:36:08.7777927',1);
CREATE TABLE IF NOT EXISTS "JobApplications" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_JobApplications" PRIMARY KEY AUTOINCREMENT,
    "JobPostId" INTEGER NOT NULL,
    "UserId" INTEGER NOT NULL,
    "CoverLetter" TEXT NULL,
    "ResumeUrl" TEXT NULL,
    "Status" TEXT NOT NULL,
    "AppliedOn" TEXT NOT NULL,
    CONSTRAINT "FK_JobApplications_JobPosts_JobPostId" FOREIGN KEY ("JobPostId") REFERENCES "JobPosts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_JobApplications_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);
INSERT INTO JobApplications VALUES(3,13,8,'<h1>Hi I am Brad Pitt</h1><ol><li>I have grip in acting</li><li>I like python</li><li>I use MacOS</li><li>I hate windows</li></ol>','eab553ac-d61b-4456-b35a-3b1afd8aa58a.pdf','Pending','2025-02-05 03:55:07.0697477');
INSERT INTO JobApplications VALUES(4,18,8,'<p>Just testing all this</p>','6e0e39dc-72ea-47ad-882b-a1c6ab2e692e.pdf','Pending','2025-02-05 15:16:21.0919209');
INSERT INTO JobApplications VALUES(5,17,8,'<p>I have good communication skills</p>','daf572ee-a32d-41c2-9f79-e935097512cd.pdf','Pending','2025-02-05 15:16:46.9747946');
INSERT INTO JobApplications VALUES(6,16,8,'<h1><strong><u>I am good at tax evasion</u></strong></h1>','f5f4704a-cf07-481d-82be-0fd55a8d1bab.pdf','Pending','2025-02-05 15:17:20.5761936');
INSERT INTO JobApplications VALUES(7,15,8,'<h3><strong><u>I sell Soap</u></strong></h3>','9d4fb35c-77c0-4a60-9066-a6e2c0a0b705.pdf','Pending','2025-02-05 15:17:47.7569978');
INSERT INTO JobApplications VALUES(8,18,14,'<h2><strong><u>I am a high school drop out</u></strong></h2>','ac3e3939-e77f-4759-ab9d-6b8776627213.pdf','Pending','2025-02-05 15:23:49.1998617');
INSERT INTO JobApplications VALUES(9,18,13,'<h1><strong><u>Harry Potter refered me for this</u></strong></h1>','4f44a6be-e8bf-4294-9ba8-278217be19ef.pdf','Interviewing','2025-02-05 15:25:29.2628709');
INSERT INTO JobApplications VALUES(10,18,12,'<h1><strong>nothing special, just testing if it works</strong></h1>','9ada4131-cae6-4579-bfac-ddff07951b16.pdf','Pending','2025-02-05 15:28:06.3163083');
CREATE TABLE IF NOT EXISTS "JobCategories" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_JobCategories" PRIMARY KEY AUTOINCREMENT,
    "AddedOn" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "UserId" INTEGER NOT NULL,
    CONSTRAINT "FK_JobCategories_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);
INSERT INTO JobCategories VALUES(1,'2025-02-02 00:41:56.9294332','other',1);
INSERT INTO JobCategories VALUES(2,'2025-02-02 00:42:43.5972832','accounts',1);
INSERT INTO JobCategories VALUES(3,'2025-02-02 00:42:53.2061689','finance',1);
INSERT INTO JobCategories VALUES(4,'2025-02-02 00:45:12.3129961','marketing',1);
INSERT INTO JobCategories VALUES(5,'2025-02-02 00:45:40.3737011','programming',1);
INSERT INTO JobCategories VALUES(6,'2025-02-02 00:47:04.5793593','sales',1);
INSERT INTO JobCategories VALUES(7,'2025-02-02 00:47:20.9042939','information technology',1);
INSERT INTO JobCategories VALUES(8,'2025-02-02 00:47:49.0499171','customer support',1);
INSERT INTO JobCategories VALUES(9,'2025-02-02 00:48:06.4626039','chat support',1);
INSERT INTO JobCategories VALUES(10,'2025-02-02 00:48:13.6509984','telesales',1);
INSERT INTO JobCategories VALUES(11,'2025-02-02 00:48:23.5490322','legal',1);
CREATE TABLE IF NOT EXISTS "JobPosts" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_JobPosts" PRIMARY KEY AUTOINCREMENT,
    "CategoryId" INTEGER NOT NULL,
    "Content" TEXT NOT NULL,
    "IsActive" INTEGER NOT NULL,
    "JobType" TEXT NOT NULL,
    "Location" TEXT NOT NULL,
    "PostDate" TEXT NOT NULL,
    "Qualification" TEXT NOT NULL,
    "Salary" REAL NOT NULL,
    "Title" TEXT NOT NULL,
    "UserId" INTEGER NOT NULL,
    CONSTRAINT "FK_JobPosts_JobCategories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "JobCategories" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_JobPosts_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);
INSERT INTO JobPosts VALUES(1,5,'<p><strong>We Require A RUST Developer who hates C/C++</strong></p>',1,'Full-Time','Lahore, Punjab','2025-02-02 00:49:53.3652015','Bachelor''s Degree',100000.0,'RUST Developer ',1);
INSERT INTO JobPosts VALUES(2,7,'<p>We are in search of a talented python developer who is expert in Zero Day Exploits and must have grip on Frameworks like FSociety, MetaSploit, BurpSuite, WireShark, Nmap and other hacking tools. Must be a fan of Mr. Robot and should have a very high typing speed</p>',1,'Part-Time','Lahore, Punjab','2025-02-02 00:54:21.9704107','Bachelor''s Degree',150000.0,'Python Developer Required',7);
INSERT INTO JobPosts VALUES(5,7,'<p><strong>Python Developer required with strong skills in django, Fast API and flask. Must have good knowledge of GitHub and git. Must have 60+ typing speed</strong></p>',1,'Full-Time','Lahore, Punjab','2025-02-02 02:40:28.5861232','Bachelor''s Degree',150000.0,'Python Developer Required',1);
INSERT INTO JobPosts VALUES(6,5,'<p>ruby on rails developer with 1 year of experience required</p>',1,'Full-Time','Karachi, Sindh','2025-02-02 02:41:18.6535084','Bachelor''s Degree',100000.0,'Ruby on Rails Developer',1);
INSERT INTO JobPosts VALUES(7,2,'<p>junior accountant with degree in ACCA or B. Com. required with min. 1 year </p>',1,'Full-Time','Islamabad, Pakistan','2025-02-02 02:43:03.8855542','Bachelor''s Degree',50000.0,'Junior Accountant',1);
INSERT INTO JobPosts VALUES(8,7,'<p>Senior Manager for the role of Dev Ops</p>',1,'Full-Time','Hyderabad, Sindh','2025-02-02 02:44:03.8542823','Master''s Degree',250000.0,'Senior DevOps Engineer',1);
INSERT INTO JobPosts VALUES(9,5,'<p>junior DBA with experience in MySQL, MongoDB, Redis and PostgreSQL required</p>',1,'Full-Time','Lahore, Punjab','2025-02-02 02:45:25.8975588','Bachelor''s Degree',75000.0,'Junior DBA',1);
INSERT INTO JobPosts VALUES(10,5,'<p>Computer Graphics Expert required, Must have grip on Blender, Unity, C#</p>',1,'Full-Time','Lahore, Punjab','2025-02-02 02:46:18.9917958','Bachelor''s Degree',150000.0,'CGI Expert',1);
INSERT INTO JobPosts VALUES(11,2,'<p>accounts manager with degree in Business Discipline and 5 years of experience required</p>',1,'Full-Time','Karachi, Sindh','2025-02-02 02:48:05.4573783','Master''s Degree',100000.0,'Accounts Manager',1);
INSERT INTO JobPosts VALUES(12,7,'<p>Manage and install CCTV cameras in office</p>',1,'Full-Time','Lahore, Punjab','2025-02-02 02:48:50.2464819','Bachelor''s Degree',75000.0,'CCTV Expert',1);
INSERT INTO JobPosts VALUES(13,5,'<h1><strong>I am looking for someone expert in django</strong></h1><ol><li>must have grip on python</li><li>must have grip on git</li><li>must have grip on docker</li><li>must be lazy</li></ol>',1,'Part-Time','Lahore, Punjab','2025-02-05 02:57:43.6297514','Bachelor''s Degree',150000.0,'Django Developer',9);
INSERT INTO JobPosts VALUES(14,7,'<ol><li><strong>Python Giga Chad </strong></li><li><strong>Must know the difference between json &amp; dictionary</strong></li><li><strong>Must hate express JS</strong></li><li><strong>Must be a fan of Andrew Tate	</strong></li></ol>',1,'Full-Time','Lahore, Punjab','2025-02-05 02:59:20.2583001','Bachelor''s Degree',150000.0,'Flask Developer',9);
INSERT INTO JobPosts VALUES(15,6,'<h1><strong>Sales manager with Mind Manipulation Skills required</strong></h1><ul><li><strong>must know how to manipulate ideas</strong></li><li><strong>must be pursuasive</strong></li><li><strong>must be determined</strong></li><li><strong>must be from Gen Z</strong></li></ul>',1,'Full-Time','Karachi, Sindh','2025-02-05 14:04:16.8766893','Bachelor''s Degree',100000.0,'Sales Manager Required',10);
INSERT INTO JobPosts VALUES(16,3,'<h1>Finance Manager with High Manipulation Skills in money laundering and tax evasion required for clean large amount of dirty money</h1><ul><li><strong>Must know how to open up LLC.</strong></li><li><strong>Must be a fan of Saul Goodman</strong></li></ul>',1,'Full-Time','Lahore, Punjab','2025-02-05 14:20:15.235534','Bachelor''s Degree',100000.0,'Finance Manager',10);
INSERT INTO JobPosts VALUES(17,8,'<h1>Manipulative Skills in Sales and marketing</h1><ul><li>Highly Punctual</li><li>Must be willing for overtimes</li><li>Must Be A fan of Wolf of Wall Street</li></ul>',1,'Part-Time','Lahore, Punjab','2025-02-05 14:24:03.0431119','High School',75000.0,'Chat Support Expert',10);
INSERT INTO JobPosts VALUES(18,4,'<h1><strong><u>Marketing Manager with high skills in sales manipulations required!</u></strong></h1>',1,'Full-Time','Karachi, Sindh','2025-02-05 15:14:03.5132691','Bachelor''s Degree',100000.0,'Marketing Manager',10);
INSERT INTO JobPosts VALUES(20,6,'<h1><strong><em>Sales staff with no experience required</em></strong></h1>',1,'Full-Time','Lahore, Punjab','2025-02-05 19:48:45.7513194','Bachelor''s Degree',50000.0,'Sales Staff Required',16);
CREATE TABLE IF NOT EXISTS "ResetPasswords" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ResetPasswords" PRIMARY KEY AUTOINCREMENT,
    "Token" TEXT NOT NULL,
    "GeneratedOn" TEXT NOT NULL,
    "Expiration" TEXT NOT NULL,
    "UserId" INTEGER NOT NULL,
    "IsUsed" INTEGER NOT NULL,
    CONSTRAINT "FK_ResetPasswords_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);
INSERT INTO ResetPasswords VALUES(1,'F0BEE471-27D6-425B-A44C-6F42983A9442','2025-02-04 00:17:30.6842512','2025-02-04 12:17:30.6842514',1,1);
CREATE TABLE IF NOT EXISTS "Newsletters" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Newsletters" PRIMARY KEY AUTOINCREMENT,
    "email" TEXT NOT NULL,
    "RegisteredOn" TEXT NOT NULL
);
INSERT INTO Newsletters VALUES(1,'usmanshahid027@outlook.com','2025-02-04 13:32:01.5189259');
INSERT INTO Newsletters VALUES(2,'tim.cook@apple.com','2025-02-06 12:59:55.7687259');
CREATE TABLE IF NOT EXISTS "Users" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY AUTOINCREMENT,
    "AboutUser" TEXT NULL,
    "Email" TEXT NULL,
    "FirstName" TEXT NOT NULL,
    "Gender" TEXT NULL,
    "IsActive" INTEGER NOT NULL,
    "IsAdmin" INTEGER NOT NULL,
    "IsStaff" INTEGER NOT NULL,
    "LastName" TEXT NOT NULL,
    "Password" TEXT NULL,
    "ProfilePic" TEXT NOT NULL,
    "RegisterDate" TEXT NOT NULL,
    "SocialLink" TEXT NULL,
    "UserRole" TEXT NOT NULL,
    "Username" TEXT NOT NULL
);
INSERT INTO Users VALUES(1,'I do computers and that stuff, just like Elliot Alderson (Mr. Robot)','usmanshahid027@outlook.com','Usman','Male',1,1,1,'Shahid','$2a$10$XEIey1ock7l5q4I2eppEG.VVmh.uUKsXZa69vtd.Vj1wyVMy5VBCW','efdc132a-befa-4478-89ca-27661f7a866b.jpg','2025-01-25 15:50:10.4377481','https://github.com/usman-s-mahmood','recruiter','usman.shahid');
INSERT INTO Users VALUES(3,'','test@mail.com','Test','Male',1,0,0,'User','$2a$10$Fp3RPW2/vrOhxkmmTo0Eoe28nsO67QScY/P1YoFNpXjqyd3elcuf2','','2025-01-25 16:29:06.6810731','','recruiter','test.user');
INSERT INTO Users VALUES(7,'Master mind of Casino And Art Stealing','danny@oceans.com','Daniel','Male',1,0,0,'Ocean','$2a$10$K/W2gTovanIu9XU./UYPaelVxusN5zguAev9QGrxBCWHV.IFkJIbi','','2025-01-31 10:52:36.121323','https://oceans.com','recruiter','danny.ocean');
INSERT INTO Users VALUES(8,'Brad Pitt, Oceans Eleven','rusty@ryan.com','Rusty','Male',1,0,0,'Ryan','$2a$10$XWNrfaElKDP6zq1O99pxZOzQLwfrrTL924yQ62RFCxXLSJGQH2Fse','3cd410f3-d5ad-4e71-a76f-eacc1aeb7be2.webp','2025-02-04 18:39:11.8536814',NULL,'applicant','rusty.ryan');
INSERT INTO Users VALUES(9,'Interstellar is my best movie','mathew@hollywood.com','Mathew','Male',1,0,0,'McConaughey','$2a$10$f0FSGhr5HHxicV3vv17bWuz1Xcl0/7KZQMc2QMnkXvvEa.VBytq8m','8bdf8337-1a1d-487b-8e96-531e148847d8.jpeg','2025-02-05 02:47:49.9902947',NULL,'recruiter','mathew.mcconaughey');
INSERT INTO Users VALUES(10,'Oceans 8, mastermind of Jewels Hunt','debie@oceans.com','Deborah','Female',1,0,0,'Ocean','$2a$10$DnEvy8/e/VEwRC3YoayG5.tnfUIyw0kvDySpWqWqot6L8q0UKompG','34eb8d13-d668-4c83-a3dd-c02e1f11bdde.webp','2025-02-05 13:56:29.6858974',NULL,'recruiter','debie.ocean');
INSERT INTO Users VALUES(11,'Literally Me','ryan@gosling.com','Ryan','Male',1,0,0,'Gosling','$2a$10$FdT4RyVAVsc97TDirQDXougpWmavUH26Oz392b9eboDQFQidmNndO','3e46acdb-5129-4849-ad06-62a952d2b9e9.jpeg','2025-02-05 15:19:44.193208',NULL,'applicant','ryan.gosling');
INSERT INTO Users VALUES(12,'From Hollywood to HireStream','jessica@alba.com','Jessica','Female',1,0,0,'Alba','$2a$10$xqq50vhTfyyKBRKAqTzrJeRVD4l6pgfiQWo4Y5xsFGvyNz8tNQ28.','71c79379-abaa-487a-94aa-cf23630e66f9.jpg','2025-02-05 15:20:32.3282206',NULL,'applicant','jess.alba');
INSERT INTO Users VALUES(13,'Famous because of Harry Potter','emma@watson.com','Emma','Female',1,0,0,'Watson','$2a$10$1ebC8AOdsVICqIaU.WzFheSDhbjWQfLdB41AqNlmytHWxWhnoLJh6','2bf72f51-daf0-4e08-815a-6138a04d3c1f.jpeg','2025-02-05 15:21:11.7884882',NULL,'applicant','emma.watson');
INSERT INTO Users VALUES(14,'Representing The ABQ, leave it at the tone Biyatch','jesse@pinkman.com','Jesse','Male',1,0,0,'Pinkman','$2a$10$aMnUeWnu1hXPpfkJzlkIY./wi884D.bo2VvnUuaoElqtLy.jHcygK','2531c63c-159f-4b7c-9d34-03048493ffc1.jpeg','2025-02-05 15:21:49.3723072',NULL,'applicant','jesse.pinkman');
INSERT INTO Users VALUES(16,'Best Batman Ever','bruce@wayne.com','Bruce','Male',1,0,0,'Wayne','$2a$10$EkBeThzxf1mpqegyy7vSuu9gWAwVBmMl0Eg01RevlozkAMyakRlVC','80402ac7-e468-46c5-ac2e-1667fcfa7287.jpg','2025-02-05 19:47:01.4163441',NULL,'recruiter','bruce.wayne');
INSERT INTO Users VALUES(17,'Best Super Hero in Marvel','dead@pool.com','Dead','Male',1,0,0,'Pool','$2a$10$w1/xsDOy8YMeL2/mDUwFD.b.fHj6F/h3Wl47vzqJZv4keVVNTzNvG','80dd911a-55ff-47d9-8682-df03b88c2f03.webp','2025-02-05 19:52:27.4016911',NULL,'applicant','dead.pool');
INSERT INTO Users VALUES(18,'Mr. Robot','elliot@alderson.com','Elliot','Male',1,0,0,'Alderson','$2a$10$9Rv7r3Nq/3ZIKsZPrK9t1edL0nAwXeY6GbmcEvggXPbUenMNsSvG.','4ff9487d-1aa4-419a-b495-955245243f1f.jpg','2025-02-05 19:57:27.7735624',NULL,'applicant','elliot.alderson');
INSERT INTO Users VALUES(19,'Ruben From Oceans','goliath@ruben.com','Goliath','Male',1,0,0,'Ruben','$2a$10$R4raKunlfHZuX.bHfutUjOpSVq7swlXupU36mTKT6OAa0P6Ju72sS','4e66bd6f-d0b1-4f40-a444-33a0b35c1a30.webp','2025-02-05 20:09:01.7229685',NULL,'applicant','goliath.ruben');
INSERT INTO Users VALUES(21,'Tumhe Toh Phansii Hogi, Phansii','acp@cid.com','ACP','Male',1,0,0,'Pradyuman','$2a$10$0S9yyqx.lvvzRA3ZeVz5auduA4FJA82BoHa7GuJBidYHNBxhXG8EG','7c0f3f24-f5b8-408c-8b89-7dc032abdf43.png','2025-02-05 23:26:43.4126264',NULL,'recruiter','acp.pradyuman');
INSERT INTO Users VALUES(22,'','delete@user.com','delete','Male',1,0,0,'user','$2a$10$thKt4eGIY08svS19HS6PqeKJp5gYm9mioaTv4RgFHDeVMT4ivoT6a','','2025-02-06 03:05:05.2218856','','recruiter','delete.user');
INSERT INTO Users VALUES(23,'','admin@admin.com','Admin','Male',1,0,0,'User','$2a$10$GMpTsUijZrkIR5CkJ.HmguC93BcgjyDFRVu1AVv.nT1YqL8y9Brza','','2025-02-07 00:27:14.5780383','','recruiter','admin.user');
DELETE FROM sqlite_sequence;
INSERT INTO sqlite_sequence VALUES('Contacts',18);
INSERT INTO sqlite_sequence VALUES('JobCategories',11);
INSERT INTO sqlite_sequence VALUES('JobPosts',21);
INSERT INTO sqlite_sequence VALUES('ResetPasswords',1);
INSERT INTO sqlite_sequence VALUES('Newsletters',2);
INSERT INTO sqlite_sequence VALUES('Users',23);
INSERT INTO sqlite_sequence VALUES('JobApplications',10);
CREATE INDEX "IX_JobApplications_JobPostId" ON "JobApplications" ("JobPostId");
CREATE INDEX "IX_JobApplications_UserId" ON "JobApplications" ("UserId");
CREATE INDEX "IX_JobCategories_UserId" ON "JobCategories" ("UserId");
CREATE INDEX "IX_JobPosts_CategoryId" ON "JobPosts" ("CategoryId");
CREATE INDEX "IX_JobPosts_UserId" ON "JobPosts" ("UserId");
CREATE INDEX "IX_ResetPasswords_UserId" ON "ResetPasswords" ("UserId");
COMMIT;
