USE master;
GO

DROP DATABASE SchoolDatabase;
GO

CREATE DATABASE SchoolDatabase;
GO

USE SchoolDatabase;
GO

CREATE TABLE  COURSE
(
	COURSE_ID			VARCHAR(100)	,
	COURSE_TITLE		VARCHAR(100)	NOT NULL,
	COURSE_DESCRIPTION	VARCHAR(100)	NOT NULL,
	COURSE_START_DATE	DATE			NOT NULL,
	COURSE_END_DATE		DATE			NOT NULL,
	NUM_OF_SLOT			INT				NOT NULL,
	CONSTRAINT PK_COURSE PRIMARY KEY(COURSE_ID),
);
GO

INSERT INTO COURSE VALUES('PRN211','Basic Cross-Platform Application Programming With .NET','COURSE_DESCRIPTION','2022-04-11','2022-04-28',30);
INSERT INTO COURSE VALUES('SWD391','Software Architecture and Design ','COURSE_DESCRIPTION','2022-01-05','2022-03-25',30);
INSERT INTO COURSE VALUES('PRM391','Mobile Programming ','COURSE_DESCRIPTION','2022-01-05','2022-03-25',30);
GO

CREATE TABLE TEACHER
(
	TEACHER_ID		VARCHAR(30)		NOT NULL,
	FIRST_NAME		VARCHAR(30)		NOT NULL,
	LAST_NAMNE		VARCHAR(30)		NOT NULL,
	DATE_OF_BIRTH	DATE			NOT NULL,
	[ADDRESS]		VARCHAR(300)	NOT NULL,
	GMAIL			NVARCHAR(255)	UNIQUE,
	[IMAGE]			VARCHAR(300)	,
	CONSTRAINT		PK_Teacher		PRIMARY KEY (Teacher_ID),
);
GO

INSERT INTO TEACHER VALUES('ChiLP','Le Phuong','Chi','1-1-1984','Demo_Address','ChiLP2@fe.edu.vn',	NULL);
INSERT INTO TEACHER VALUES('TEACHER_1','First_Name_1','LAST_NAME_1','1-1-1984','Demo_Address_1','DEMO_GMAIL_1',	NULL);
INSERT INTO TEACHER VALUES('TEACHER_2','First_Name_2','LAST_NAME_2','1-1-1984','Demo_Address_2','DEMO_GMAIL_2',	NULL);
GO

CREATE TABLE STUDENT
(
	ROLLNUMBER		VARCHAR(30)		NOT NULL,
	FIRST_NAME		VARCHAR(30)		NOT NULL,
	LAST_NAME		VARCHAR(30)		NOT NULL,
	DATE_OF_BIRTH	DATE			NOT NULL,
	[ADDRESS]		VARCHAR(300)	,
	[IMAGE]			VARCHAR(300)	,
	CONSTRAINT		PK_STUDENT		PRIMARY KEY (ROLLNUMBER),	
);
GO

INSERT INTO STUDENT VALUES('HE0001','FIRST_NAME_1','LAST_NAME_1','2000-01-01','ADDRESS_1',NULL);
INSERT INTO STUDENT VALUES('HE0002','FIRST_NAME_2','LAST_NAME_2','2000-01-02','ADDRESS_2',NULL);
INSERT INTO STUDENT VALUES('HE0003','FIRST_NAME_3','LAST_NAME_3','2000-01-03','ADDRESS_3',NULL);
INSERT INTO STUDENT VALUES('HE0004','FIRST_NAME_4','LAST_NAME_4','2000-01-04','ADDRESS_4',NULL);
INSERT INTO STUDENT VALUES('HE0005','FIRST_NAME_5','LAST_NAME_5','2000-01-05','ADDRESS_5',NULL);
INSERT INTO STUDENT VALUES('HE0006','FIRST_NAME_6','LAST_NAME_6','2000-01-06','ADDRESS_6',NULL);
INSERT INTO STUDENT VALUES('HE0007','FIRST_NAME_7','LAST_NAME_7','2000-01-07','ADDRESS_7',NULL);
INSERT INTO STUDENT VALUES('HE0008','FIRST_NAME_8','LAST_NAME_8','2000-01-08','ADDRESS_8',NULL);
INSERT INTO STUDENT VALUES('HE0009','FIRST_NAME_9','LAST_NAME_9','2000-01-09','ADDRESS_9',NULL);
INSERT INTO STUDENT VALUES('HE0010','FIRST_NAME_10','LAST_NAME_10','2000-01-10','ADDRESS_10',NULL);
GO

CREATE TABLE STUDENT_GROUP
(
	STUDENT_GROUP_ID		VARCHAR(30)		NOT NULL,	
	STUDENT_GROUP_NAME		VARCHAR(30)		NOT NULL,
	CONSTRAINT		PK_STUDENT_GROUP	PRIMARY KEY (STUDENT_GROUP_ID),
);
GO

INSERT INTO STUDENT_GROUP VALUES('PRN211.M-BL5','GROUP_BLOCK_5');
INSERT INTO STUDENT_GROUP VALUES('PRN211.E-BL5','GROUP_BLOCK_5');
INSERT INTO STUDENT_GROUP VALUES('GROUP_1','GROUP_NAME_1');
INSERT INTO STUDENT_GROUP VALUES('GROUP_2','GROUP_NAME_2');
GO

CREATE TABLE STUDENT_GROUP_DETAIL
(
	STUDENT_GROUP_ID	VARCHAR(30)		NOT NULL,	
	STUDENT_ID			VARCHAR(30)		NOT NULL,
	CONSTRAINT		PK_STUDENT_GROUP_DETAIL	PRIMARY KEY (STUDENT_GROUP_ID,STUDENT_ID),
	CONSTRAINT		FK_STUDENT_ID		FOREIGN KEY	(STUDENT_ID)		REFERENCES STUDENT(ROLLNUMBER),
	CONSTRAINT		FK_STUDENT_GROUP_ID	FOREIGN KEY	(STUDENT_GROUP_ID)	REFERENCES STUDENT_GROUP(STUDENT_GROUP_ID),
);
GO

INSERT INTO STUDENT_GROUP_DETAIL VALUES('PRN211.M-BL5','HE0001');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('PRN211.M-BL5','HE0002');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('PRN211.M-BL5','HE0003');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('PRN211.M-BL5','HE0004');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('PRN211.M-BL5','HE0005');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('GROUP_1','HE0001');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('GROUP_1','HE0002');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('GROUP_1','HE0003');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('GROUP_1','HE0004');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('GROUP_1','HE0005');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('GROUP_2','HE0006');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('GROUP_2','HE0007');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('GROUP_2','HE0008');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('GROUP_2','HE0009');
INSERT INTO STUDENT_GROUP_DETAIL VALUES('GROUP_2','HE0010');
GO

CREATE TABLE COURSE_DETAIL
(
	COURSE_ID			VARCHAR(100)	,
	SLOT_ID				VARCHAR(30)		UNIQUE,
	TEACHER_ID			VARCHAR(30)		NOT NULL,
	STUDENT_GROUP_ID	VARCHAR(30)		NOT NULL,
	CONSTRAINT PK_COURSE_DETAIL	PRIMARY KEY(COURSE_ID,SLOT_ID),
	CONSTRAINT FK_TEACHER_ID	FOREIGN KEY (TEACHER_ID) REFERENCES TEACHER(TEACHER_ID),
	CONSTRAINT FK_COURSE_DETAIL_STUDENT_GROUP_ID	FOREIGN KEY (STUDENT_GROUP_ID)	REFERENCES STUDENT_GROUP(STUDENT_GROUP_ID),
	CONSTRAINT FK_COURSE_DETAIL_COURSE	FOREIGN KEY (COURSE_ID)	REFERENCES COURSE(COURSE_ID),
);
GO

INSERT INTO COURSE_DETAIL VALUES('PRN211','PRN211_M-BL5','ChiLP','PRN211.M-BL5');
INSERT INTO COURSE_DETAIL VALUES('SWD391','SLOT_ID_1','TEACHER_1','GROUP_1');

GO

CREATE TABLE SLOT_TIME
(
	SLOT_TIME_NAME		INT				IDENTITY(1,1),
	SLOT_DATE			VARCHAR(30)		NOT NULL,
	END_TIME			VARCHAR(30)		NOT NULL,
	CONSTRAINT PK_SLOT_TIME_DETAIL		PRIMARY KEY (SLOT_TIME_NAME),	
);
GO

INSERT INTO SLOT_TIME VALUES('7:30','9:00');
	INSERT INTO SLOT_TIME VALUES('9:10','10:40');
	INSERT INTO SLOT_TIME VALUES('10:50','12:20');
	INSERT INTO SLOT_TIME VALUES('12:50','14:20');
	INSERT INTO SLOT_TIME VALUES('14:30','16:00');
	INSERT INTO SLOT_TIME VALUES('16:10','17:40');
	INSERT INTO SLOT_TIME VALUES('17:50','19:20');
	INSERT INTO SLOT_TIME VALUES('19:30','21:00');
GO

CREATE TABLE SLOT_INFORMATION
(
	SLOT_ID			VARCHAR(30)		NOT NULL,
	SLOT_STATUS		BIT				,
	SLOT_NOTE		VARCHAR(300)	NOT NULL,
	SLOT_DATE		DATE			NOT NULL,
	SLOT_TIME_NAME	INT				NOT NULL,
	CONSTRAINT PK_SLOT_INFORMATION			PRIMARY KEY (SLOT_ID,SLOT_DATE,SLOT_TIME_NAME),
	CONSTRAINT FK_SLOT_TIME_NAME			FOREIGN KEY	(SLOT_TIME_NAME)	REFERENCES SLOT_TIME(SLOT_TIME_NAME),
	CONSTRAINT FK_SLOT_INFORMATION_SLOT_ID	FOREIGN KEY	(SLOT_ID)			REFERENCES COURSE_DETAIL(SLOT_ID),
);
GO
/*
INSERT INTO SLOT_INFORMATION VALUES('PRN211_M-BL5',NULL,'NOT STARTED','2022-04-11',2);
INSERT INTO SLOT_INFORMATION VALUES('PRN211_M-BL5',NULL,'NOT STARTED','2022-04-11',3);
INSERT INTO SLOT_INFORMATION VALUES('PRN211_M-BL5',NULL,'NOT STARTED','2022-04-12',2);
INSERT INTO SLOT_INFORMATION VALUES('PRN211_M-BL5',NULL,'NOT STARTED','2022-04-12',3);
INSERT INTO SLOT_INFORMATION VALUES('PRN211_M-BL5',NULL,'NOT STARTED','2022-04-13',2);
INSERT INTO SLOT_INFORMATION VALUES('PRN211_M-BL5',NULL,'NOT STARTED','2022-04-13',3);
INSERT INTO SLOT_INFORMATION VALUES('PRN211_M-BL5',NULL,'NOT STARTED','2022-04-14',2);
INSERT INTO SLOT_INFORMATION VALUES('PRN211_M-BL5',NULL,'NOT STARTED','2022-04-14',3);
INSERT INTO SLOT_INFORMATION VALUES('PRN211_M-BL5',NULL,'NOT STARTED','2022-04-15',2);
INSERT INTO SLOT_INFORMATION VALUES('PRN211_M-BL5',NULL,'NOT STARTED','2022-04-15',3);
INSERT INTO SLOT_INFORMATION VALUES('SLOT_ID_1',NULL,'NOT STARTED','2022-04-12',4);
INSERT INTO SLOT_INFORMATION VALUES('SLOT_ID_1',NULL,'NOT STARTED','2022-04-12',5);
INSERT INTO SLOT_INFORMATION VALUES('SLOT_ID_1',NULL,'NOT STARTED','2022-04-14',4);
GO
*/
CREATE TABLE SLOT_ATTENDANCE
(
	SLOT_ID				VARCHAR(30)		NOT NULL,
	STUDENT_GROUP_ID	VARCHAR(30)		NOT NULL,
	STUDENT_ID			VARCHAR(30)		NOT NULL,
	ATTENDANCE			BIT				,
	NOTE				VARCHAR(300)	NOT NULL,
	SLOT_DATE			DATE			NOT NULL,
	SLOT_TIME_NAME		INT				NOT NULL,
	CONSTRAINT PK_SLOT_ATTENDANCE				PRIMARY KEY (SLOT_ID,STUDENT_ID),
	CONSTRAINT FK_SLOT_ATTENDANCE				FOREIGN KEY (STUDENT_GROUP_ID,STUDENT_ID)	REFERENCES STUDENT_GROUP_DETAIL(STUDENT_GROUP_ID,STUDENT_ID),
	CONSTRAINT FK_SLOT_ATTENDANCE_SLOT_DETAI	FOREIGN KEY (SLOT_ID,SLOT_DATE,SLOT_TIME_NAME) REFERENCES SLOT_INFORMATION(SLOT_ID,SLOT_DATE,SLOT_TIME_NAME),
);
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ACCOUNT](
	[USERNAME] [nvarchar](30) NOT NULL,
	[PASSWORD] [nvarchar](30) NOT NULL,
	[ROLEID] [int] NOT NULL,
	[ID] [varchar](30) NOT NULL,
 CONSTRAINT [PK_ACCOUNT_STUDENT] PRIMARY KEY CLUSTERED 
(
	[USERNAME] ASC,
	[ROLEID] ASC,
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO ACCOUNT VALUES('Student1','Student1',1,'HE0001');
INSERT INTO ACCOUNT VALUES('Student2','Student2',1,'HE0002');
INSERT INTO ACCOUNT VALUES('Student3','Student3',1,'HE0003');
INSERT INTO ACCOUNT VALUES('Teacher3','Teacher3',1,'ChiLP');
























