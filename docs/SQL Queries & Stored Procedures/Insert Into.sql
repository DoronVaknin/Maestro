--Region
INSERT INTO Region VALUES ('צפון');

INSERT INTO Region VALUES ('חיפה');

INSERT INTO Region VALUES ('שרון');

INSERT INTO Region VALUES ('מרכז');

INSERT INTO Region VALUES ('שומרון');

INSERT INTO Region VALUES ('ירושלים');

INSERT INTO Region VALUES ('דרום ואילת');

--Customer
INSERT INTO Customer VALUES (
'123456789',
'דורון',
'וקנין',
'הטייסים 11, חדרה, ישראל',
'046313154',
'0523693728',
'046313155',
'doronvaknin123@gmail.com',
'3'
);
GO

INSERT INTO Customer VALUES (
'111111111',
'שמוליק',
'חזן',
'אלי כהן 5, חדרה, ישראל',
'046123456',
'0524963627',
'046975421',
'shmulikhazan123@gmail.com',
'1'
);

INSERT INTO Customer VALUES (
'222222222',
'ליאור',
'זיני',
'השופטים 13, הרצליה, ישראל',
'046577429',
'0524380247',
'046248986',
'liorzini123@gmail.com',
'4'
);

INSERT INTO Customer VALUES (
'123123123',
'ליונל',
'מסי',
'דיזינגוף 36, תל אביב-יפו',
'032394877',
'0547649885',
'032342987',
'lionelmessi123@gmail.com',
'4'
);
GO

--Project Status
INSERT INTO ProjStatus VALUES ('הצעת מחיר');
INSERT INTO ProjStatus VALUES ('הזמנת עבודה');
INSERT INTO ProjStatus VALUES ('משקוף עיוור');
INSERT INTO ProjStatus VALUES ('סגירת פרטים');
INSERT INTO ProjStatus VALUES ('הזמנת חומר');
INSERT INTO ProjStatus VALUES ('מדידה לייצור');
INSERT INTO ProjStatus VALUES ('ייצור');
INSERT INTO ProjStatus VALUES ('התקנה');
INSERT INTO ProjStatus VALUES ('סיום פרויקט');
GO

--Project
INSERT INTO Project VALUES (
'דורון וקנין - חדרה',
'הערות',
'2012-09-11',
'2018-07-17',
'2012-11-13',
'7000.35',
'יוסי',
'0544873209',
'דני',
'0526822490',
'משה',
'0522874388',
'http://proj.ruppin.ac.il/igroup9/prod/images/hatches.jpg',
'123456789',
'4'
);

INSERT INTO Project VALUES (
'שמוליק חזן - חדרה',
'הערות',
'2013-04-27',
'2020-04-24',
'2013-06-29',
'6000.24',
'בני',
'0542348979',
'יהודה',
'0529864423',
'אלי',
'0528734773',
'http://proj.ruppin.ac.il/igroup9/prod/images/hatches.jpg',
'111111111',
'6'
);

INSERT INTO Project VALUES (
'ליאור זיני - חדרה',
'הערות',
'2016-06-12',
'2020-04-24',
'',
'8000.99',
'קובי',
'0525475784',
'דורון',
'0525348580',
'אורן',
'0540978344',
'http://proj.ruppin.ac.il/igroup9/prod/images/hatches.jpg',
'222222222',
'6'
);

INSERT INTO Project VALUES (
'ליונל מסי - תל אביב',
'הערות',
'2013-05-13',
'2020-04-24',
'2013-07-15',
'4000',
'רמי',
'0527872344',
'דניאל',
'0521234232',
'עמית',
'0525648900',
'http://proj.ruppin.ac.il/igroup9/prod/images/hatches.jpg',
'123123123',
'1'
);
GO

--Employee
INSERT INTO Employee VALUES (
'038124123',
'שמעון ימין',
'מנהל התקנות',
'ShimonY',
'046221773',
'0524268422'
);

INSERT INTO Employee VALUES (
'034982393',
'מלי ימין',
'מנהלת מכירות',
'MaliY',
'046221773',
'0522398728'
);

INSERT INTO Employee VALUES (
'302042267',
'בטי אשורוב',
'מנהלת טכנית',
'BettiY',
'046221773',
'0521230983'
);

INSERT INTO Employee VALUES (
'302224167',
'ישראל ישראלי',
'עובד ייצור',
'Israel',
'046221773',
'0542897872'
);

INSERT INTO Employee VALUES (
'300843637',
'ויקטור יושבייב',
'עובד ייצור',
'VictorY',
'046221773',
'0527203487'
);

INSERT INTO Employee VALUES (
'377234299',
'אלכס בינוביץ',
'עובד ייצור',
'AlexB',
'046221773',
'0540900989'
);
GO

--Service Call
INSERT INTO ServiceCall VALUES (
'הגשם דולף מהחלון',
'1',
'2010-11-09',
NULL,
'123456789',
'1'
);

INSERT INTO ServiceCall VALUES (
'הדלת חורקת בצורה בלתי נסבלת',
'0',
'2013-08-12',
NULL,
'111111111',
'2'
);

INSERT INTO ServiceCall VALUES (
'החלון לא נסגר בקלות',
'0',
'2011-05-16',
NULL,
'123123123',
'3'
);
GO

--Supplier
INSERT INTO Supplier VALUES (
'אור תריס',
'החרושת 3, קרית ביאליק',
'0723223481',
'0542423433',
'046212392',
'ortris@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'כלבו לתריס',
'הרקפת 8, קרית חיים',
'046312300',
'0523539333',
'046370297',
'kolbo@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'GTO',
'החרושת 3, חדרה',
'0720980980',
'',
'046254392',
'gto@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'OPR',
'החרושת 3, תל אביב-יפו',
'0724333481',
'',
'046212892',
'opr@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'צאלון',
'החרושת 3, חולון',
'0723194381',
'0544423433',
'',
'tseelon@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'אין גלאס',
'החרושת 3, אשדוד',
'0723211481',
'054233333',
'',
'inglass@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'בליסדר',
'החרושת 3, נתניה',
'0723200481',
'0542423111',
'046227452',
'bliseder@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'אור אלום',
'החרושת 3, קרית חיים',
'0721313481',
'0542555433',
'0462166652',
'oralum@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'כלבו',
'החרושת 3, תל אביב-יפו',
'0723290481',
'0542488833',
'036211192',
'kolbo2@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'צמד',
'החרושת 3, חדרה',
'0723200481',
'0522423123',
'046242392',
'tsemed@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'סומפי',
'החרושת 3, ראשון לציון',
'072327781',
'',
'046212792',
'somfy@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'שפר',
'החרושת 3, אבן יהודה',
'',
'0547423433',
'046212397',
'shefer@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'סטאר גלאס',
'החרושת 3, סכנין',
'0723204381',
'0542482533',
'',
'starglass@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'גולן זכוכית',
'החרושת 3, תל אביב-יפו',
'0721583481',
'0542400023',
'046215292',
'golanglass@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'IMA',
'החרושת 3, ראשון לציון',
'0723223741',
'0542413433',
'046214392',
'ima@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'Open Art',
'החרושת 3, אור עקיבא',
'0723223081',
'0548423433',
'046215392',
'openart@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'Framed Box',
'הכידון 5, חיפה',
'0728975581',
'',
'046123111',
'framedbox@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'גולן דיזיין',
'בעלי המלאכה 23, חדרה',
'0722090199',
'0523876834',
'',
'golandesign@gmail.com',
'true'
);
GO

--RawMaterials
INSERT INTO RawMaterials VALUES('תריסים');
INSERT INTO RawMaterials VALUES('נאספים');
INSERT INTO RawMaterials VALUES('אלומיניום');
INSERT INTO RawMaterials VALUES('וואלים');
INSERT INTO RawMaterials VALUES('יואים');
INSERT INTO RawMaterials VALUES('פרזול');
INSERT INTO RawMaterials VALUES('מנועים');
INSERT INTO RawMaterials VALUES('ממ"ד');
INSERT INTO RawMaterials VALUES('זכוכית');
INSERT INTO RawMaterials VALUES('ארגזים');
GO

--SupplierRawMaterials
INSERT INTO SupplierRawMaterials VALUES ('1','1');
INSERT INTO SupplierRawMaterials VALUES ('2','1');
INSERT INTO SupplierRawMaterials VALUES ('3','1');
INSERT INTO SupplierRawMaterials VALUES ('4','2');
INSERT INTO SupplierRawMaterials VALUES ('5','2');
INSERT INTO SupplierRawMaterials VALUES ('6','2');
INSERT INTO SupplierRawMaterials VALUES ('7','3');
INSERT INTO SupplierRawMaterials VALUES ('8','3');
INSERT INTO SupplierRawMaterials VALUES ('9','3');
INSERT INTO SupplierRawMaterials VALUES ('7','4');
INSERT INTO SupplierRawMaterials VALUES ('8','4');
INSERT INTO SupplierRawMaterials VALUES ('9','4');
INSERT INTO SupplierRawMaterials VALUES ('7','5');
INSERT INTO SupplierRawMaterials VALUES ('8','5');
INSERT INTO SupplierRawMaterials VALUES ('9','5');
INSERT INTO SupplierRawMaterials VALUES ('10','6');
INSERT INTO SupplierRawMaterials VALUES ('11','6');
INSERT INTO SupplierRawMaterials VALUES ('10','7');
INSERT INTO SupplierRawMaterials VALUES ('11','7');
INSERT INTO SupplierRawMaterials VALUES ('12','8');
INSERT INTO SupplierRawMaterials VALUES ('13','9');
INSERT INTO SupplierRawMaterials VALUES ('14','9');
INSERT INTO SupplierRawMaterials VALUES ('15','9');
INSERT INTO SupplierRawMaterials VALUES ('16','10');
INSERT INTO SupplierRawMaterials VALUES ('17','10');
INSERT INTO SupplierRawMaterials VALUES ('18','10');
GO

--OrderStatus
INSERT INTO OrderStatus VALUES ('הוזמן');
INSERT INTO OrderStatus VALUES ('מתעכב');
INSERT INTO OrderStatus VALUES ('התקבל');
GO

--Orders
INSERT INTO Orders VALUES (
'2011-06-12',
'2011-08-12',
'2011-08-06',
'5',
'1',
'1',
'2',
'1'
);

INSERT INTO Orders VALUES (
'2010-03-11',
'2010-04-22',
'2010-06-11',
'3',
'2',
'2',
'6',
'2'
);

INSERT INTO Orders VALUES (
'2013-06-12',
'2013-12-31',
'2014-01-06',
'4',
'3',
'3',
'16',
'9'
);
GO

--HatchType
INSERT INTO HatchType VALUES ('חלון');
INSERT INTO HatchType VALUES ('דלת');
GO

--HatchStatus
INSERT INTO HatchStatus VALUES ('ייצור');
INSERT INTO HatchStatus VALUES ('תקלה');
INSERT INTO HatchStatus VALUES ('הושלם');
INSERT INTO HatchStatus VALUES ('הועמס');
INSERT INTO HatchStatus VALUES ('הותקן');
INSERT INTO HatchStatus VALUES ('פינישים');
GO

--FailureType
INSERT INTO FailureType VALUES ('חו"ג חסר');
INSERT INTO FailureType VALUES ('פגם בפרזול');
INSERT INTO FailureType VALUES ('פגם בפרופיל');
INSERT INTO FailureType VALUES ('סדק בזכוכית');
INSERT INTO FailureType VALUES ('שבר בזכוכית');
GO

--Failure
--INSERT INTO Failure VALUES ('1','סדק בפינה הימנית');
--INSERT INTO Failure VALUES ('1','');
--INSERT INTO Failure VALUES ('2','הערות למיניהם');
--INSERT INTO Failure VALUES ('2','חסר פרופיל בלגי 40 ס"מ');
--GO

--Hatch
INSERT INTO Hatch VALUES ('2','2','1','1','302224167','2011-05-04','פירוט תקלה','true');
INSERT INTO Hatch VALUES ('3','2','1','5','300843637','','','true');
INSERT INTO Hatch VALUES ('2','1','1','3','302224167','2013-07-12','פירוט תקלה','true');
INSERT INTO Hatch VALUES ('1','2','2','2','377234299','2010-03-08','','true');
INSERT INTO Hatch VALUES ('2','2','2','1','377234299','2011-08-09','חסר פרופיל בלגי 40 ס"מ','true');
INSERT INTO Hatch VALUES ('1','2','2',null,'302224167','','','true');
INSERT INTO Hatch VALUES ('1','1','3','3','300843637','2013-03-11','','true');
INSERT INTO Hatch VALUES ('2','2','3','1','377234299','2014-06-03','פירוט תקלה','true');
INSERT INTO Hatch VALUES ('3','2','3','4','302224167','2012-07-02','','true');
INSERT INTO Hatch VALUES ('1','1','4','1','300843637','','','true');
INSERT INTO Hatch VALUES ('3','1','4',null,'377234299','2010-09-10','','true');
INSERT INTO Hatch VALUES ('2','1','4','5','302224167','2011-10-11','פירוט תקלה','true');
GO

--QAQuestion
INSERT INTO QAQuestion VALUES ('שימון צירים');
INSERT INTO QAQuestion VALUES ('כיוון צירים');
INSERT INTO QAQuestion VALUES ('נעילה');
INSERT INTO QAQuestion VALUES ('אטימות');
INSERT INTO QAQuestion VALUES ('תקינות התריס');
INSERT INTO QAQuestion VALUES ('טווח פתיחה');
INSERT INTO QAQuestion VALUES ('רשת');
INSERT INTO QAQuestion VALUES ('תקינות מנוע');
GO
-- SO FAR EVERYTHING OK

--QA - בעיה - צריך להיות לפתח מספר שאלות בניגוד למצב הנ"ל
INSERT INTO QA VALUES ('1','1','0','0');
INSERT INTO QA VALUES ('2','2','1','1');
INSERT INTO QA VALUES ('3','5','1','0');
INSERT INTO QA VALUES ('4','7','0','0');
INSERT INTO QA VALUES ('5','1','0','0');
INSERT INTO QA VALUES ('6','3','1','1');
GO

--Picture
INSERT INTO Picture VALUES (
'צילום פתח הסלון',
'2013-12-31',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'1'
);

INSERT INTO Picture VALUES (
'צילום פתח המטבח',
'2010-10-23',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'2'
);

INSERT INTO Picture VALUES (
'צילום תריס נגלל',
'2014-06-11',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'3'
);

INSERT INTO Picture VALUES (
'צילום פרגולה חיצוני',
'2010-12-21',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'4'
);

INSERT INTO Picture VALUES (
'צילום פרגולה פנימי',
'2011-10-22',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'5'
);

INSERT INTO Picture VALUES (
'דלת חדר 1',
'2012-09-12',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'6'
);
GO

--Pin
INSERT INTO Pin VALUES (
'30.03',
'43.12',
'שבר! יש להזמין פרופיל חדש',
'audio/sample1.mp3',
'video/sample1.avi',
'1'
);

INSERT INTO Pin VALUES (
'89.9',
'55.52',
'קרע ברשת! להזמין חדשה',
'audio/sample2.mp3',
'video/sample2.avi',
'2'
);

INSERT INTO Pin VALUES (
'35.21',
'49.17',
'לא לשכוח לכוון צירים',
'audio/sample3.mp3',
'video/sample3.avi',
'3'
);

INSERT INTO Pin VALUES (
'27.04',
'56.12',
'לתקן סדק',
'audio/sample4.mp3',
'video/sample4.avi',
'4'
);

INSERT INTO Pin VALUES (
'82.9',
'57.52',
'לשמן צירים',
'audio/sample5.mp3',
'video/sample5.avi',
'5'
);

INSERT INTO Pin VALUES (
'31.76',
'43.37',
'',
'audio/sample6.mp3',
'video/sample6.avi',
'6'
);
GO