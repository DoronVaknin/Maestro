--Region
INSERT INTO Region VALUES ('����');

INSERT INTO Region VALUES ('����');

INSERT INTO Region VALUES ('����');

INSERT INTO Region VALUES ('����');

INSERT INTO Region VALUES ('������');

INSERT INTO Region VALUES ('�������');

INSERT INTO Region VALUES ('���� �����');

--Customer
INSERT INTO Customer VALUES (
'123456789',
'�����',
'�����',
'������� 11, ����, �����',
'046313154',
'0523693728',
'046313155',
'doronvaknin123@gmail.com',
'3'
);
GO

INSERT INTO Customer VALUES (
'111111111',
'������',
'���',
'��� ��� 5, ����, �����',
'046123456',
'0524963627',
'046975421',
'shmulikhazan123@gmail.com',
'1'
);

INSERT INTO Customer VALUES (
'222222222',
'�����',
'����',
'������� 13, ������, �����',
'046577429',
'0524380247',
'046248986',
'liorzini123@gmail.com',
'4'
);

INSERT INTO Customer VALUES (
'123123123',
'�����',
'���',
'�������� 36, �� ����-���',
'032394877',
'0547649885',
'032342987',
'lionelmessi123@gmail.com',
'4'
);
GO

--Project Status
INSERT INTO ProjStatus VALUES ('���� ����');
INSERT INTO ProjStatus VALUES ('����� �����');
INSERT INTO ProjStatus VALUES ('����� �����');
INSERT INTO ProjStatus VALUES ('����� �����');
INSERT INTO ProjStatus VALUES ('����� ����');
INSERT INTO ProjStatus VALUES ('����� ������');
INSERT INTO ProjStatus VALUES ('�����');
INSERT INTO ProjStatus VALUES ('�����');
INSERT INTO ProjStatus VALUES ('���� ������');
GO

--Project
INSERT INTO Project VALUES (
'����� ����� - ����',
'�����',
'2012-09-11',
'2018-07-17',
'2012-11-13',
'7000.35',
'����',
'0544873209',
'���',
'0526822490',
'���',
'0522874388',
'http://proj.ruppin.ac.il/igroup9/prod/images/hatches.jpg',
'123456789',
'4'
);

INSERT INTO Project VALUES (
'������ ��� - ����',
'�����',
'2013-04-27',
'2020-04-24',
'2013-06-29',
'6000.24',
'���',
'0542348979',
'�����',
'0529864423',
'���',
'0528734773',
'http://proj.ruppin.ac.il/igroup9/prod/images/hatches.jpg',
'111111111',
'6'
);

INSERT INTO Project VALUES (
'����� ���� - ����',
'�����',
'2016-06-12',
'2020-04-24',
'',
'8000.99',
'����',
'0525475784',
'�����',
'0525348580',
'����',
'0540978344',
'http://proj.ruppin.ac.il/igroup9/prod/images/hatches.jpg',
'222222222',
'6'
);

INSERT INTO Project VALUES (
'����� ��� - �� ����',
'�����',
'2013-05-13',
'2020-04-24',
'2013-07-15',
'4000',
'���',
'0527872344',
'�����',
'0521234232',
'����',
'0525648900',
'http://proj.ruppin.ac.il/igroup9/prod/images/hatches.jpg',
'123123123',
'1'
);
GO

--Employee
INSERT INTO Employee VALUES (
'038124123',
'����� ����',
'���� ������',
'ShimonY',
'046221773',
'0524268422'
);

INSERT INTO Employee VALUES (
'034982393',
'��� ����',
'����� ������',
'MaliY',
'046221773',
'0522398728'
);

INSERT INTO Employee VALUES (
'302042267',
'��� ������',
'����� �����',
'BettiY',
'046221773',
'0521230983'
);

INSERT INTO Employee VALUES (
'302224167',
'����� ������',
'���� �����',
'Israel',
'046221773',
'0542897872'
);

INSERT INTO Employee VALUES (
'300843637',
'������ �������',
'���� �����',
'VictorY',
'046221773',
'0527203487'
);

INSERT INTO Employee VALUES (
'377234299',
'���� �������',
'���� �����',
'AlexB',
'046221773',
'0540900989'
);
GO

--Service Call
INSERT INTO ServiceCall VALUES (
'���� ���� ������',
'1',
'2010-11-09',
NULL,
'123456789',
'1'
);

INSERT INTO ServiceCall VALUES (
'���� ����� ����� ���� �����',
'0',
'2013-08-12',
NULL,
'111111111',
'2'
);

INSERT INTO ServiceCall VALUES (
'����� �� ���� �����',
'0',
'2011-05-16',
NULL,
'123123123',
'3'
);
GO

--Supplier
INSERT INTO Supplier VALUES (
'��� ����',
'������ 3, ���� ������',
'0723223481',
'0542423433',
'046212392',
'ortris@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'���� �����',
'����� 8, ���� ����',
'046312300',
'0523539333',
'046370297',
'kolbo@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'GTO',
'������ 3, ����',
'0720980980',
'',
'046254392',
'gto@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'OPR',
'������ 3, �� ����-���',
'0724333481',
'',
'046212892',
'opr@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'�����',
'������ 3, �����',
'0723194381',
'0544423433',
'',
'tseelon@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'��� ����',
'������ 3, �����',
'0723211481',
'054233333',
'',
'inglass@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'������',
'������ 3, �����',
'0723200481',
'0542423111',
'046227452',
'bliseder@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'��� ����',
'������ 3, ���� ����',
'0721313481',
'0542555433',
'0462166652',
'oralum@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'����',
'������ 3, �� ����-���',
'0723290481',
'0542488833',
'036211192',
'kolbo2@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'���',
'������ 3, ����',
'0723200481',
'0522423123',
'046242392',
'tsemed@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'�����',
'������ 3, ����� �����',
'072327781',
'',
'046212792',
'somfy@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'���',
'������ 3, ��� �����',
'',
'0547423433',
'046212397',
'shefer@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'���� ����',
'������ 3, �����',
'0723204381',
'0542482533',
'',
'starglass@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'���� ������',
'������ 3, �� ����-���',
'0721583481',
'0542400023',
'046215292',
'golanglass@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'IMA',
'������ 3, ����� �����',
'0723223741',
'0542413433',
'046214392',
'ima@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'Open Art',
'������ 3, ��� �����',
'0723223081',
'0548423433',
'046215392',
'openart@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'Framed Box',
'������ 5, ����',
'0728975581',
'',
'046123111',
'framedbox@gmail.com',
'true'
);

INSERT INTO Supplier VALUES (
'���� ������',
'���� ������ 23, ����',
'0722090199',
'0523876834',
'',
'golandesign@gmail.com',
'true'
);
GO

--RawMaterials
INSERT INTO RawMaterials VALUES('������');
INSERT INTO RawMaterials VALUES('������');
INSERT INTO RawMaterials VALUES('���������');
INSERT INTO RawMaterials VALUES('������');
INSERT INTO RawMaterials VALUES('�����');
INSERT INTO RawMaterials VALUES('�����');
INSERT INTO RawMaterials VALUES('������');
INSERT INTO RawMaterials VALUES('��"�');
INSERT INTO RawMaterials VALUES('������');
INSERT INTO RawMaterials VALUES('������');
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
INSERT INTO OrderStatus VALUES ('�����');
INSERT INTO OrderStatus VALUES ('�����');
INSERT INTO OrderStatus VALUES ('�����');
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
INSERT INTO HatchType VALUES ('����');
INSERT INTO HatchType VALUES ('���');
GO

--HatchStatus
INSERT INTO HatchStatus VALUES ('�����');
INSERT INTO HatchStatus VALUES ('����');
INSERT INTO HatchStatus VALUES ('�����');
INSERT INTO HatchStatus VALUES ('�����');
INSERT INTO HatchStatus VALUES ('�����');
INSERT INTO HatchStatus VALUES ('�������');
GO

--FailureType
INSERT INTO FailureType VALUES ('��"� ���');
INSERT INTO FailureType VALUES ('��� ������');
INSERT INTO FailureType VALUES ('��� �������');
INSERT INTO FailureType VALUES ('��� �������');
INSERT INTO FailureType VALUES ('��� �������');
GO

--Failure
--INSERT INTO Failure VALUES ('1','��� ����� ������');
--INSERT INTO Failure VALUES ('1','');
--INSERT INTO Failure VALUES ('2','����� �������');
--INSERT INTO Failure VALUES ('2','��� ������ ���� 40 �"�');
--GO

--Hatch
INSERT INTO Hatch VALUES ('2','2','1','1','302224167','2011-05-04','����� ����','true');
INSERT INTO Hatch VALUES ('3','2','1','5','300843637','','','true');
INSERT INTO Hatch VALUES ('2','1','1','3','302224167','2013-07-12','����� ����','true');
INSERT INTO Hatch VALUES ('1','2','2','2','377234299','2010-03-08','','true');
INSERT INTO Hatch VALUES ('2','2','2','1','377234299','2011-08-09','��� ������ ���� 40 �"�','true');
INSERT INTO Hatch VALUES ('1','2','2',null,'302224167','','','true');
INSERT INTO Hatch VALUES ('1','1','3','3','300843637','2013-03-11','','true');
INSERT INTO Hatch VALUES ('2','2','3','1','377234299','2014-06-03','����� ����','true');
INSERT INTO Hatch VALUES ('3','2','3','4','302224167','2012-07-02','','true');
INSERT INTO Hatch VALUES ('1','1','4','1','300843637','','','true');
INSERT INTO Hatch VALUES ('3','1','4',null,'377234299','2010-09-10','','true');
INSERT INTO Hatch VALUES ('2','1','4','5','302224167','2011-10-11','����� ����','true');
GO

--QAQuestion
INSERT INTO QAQuestion VALUES ('����� �����');
INSERT INTO QAQuestion VALUES ('����� �����');
INSERT INTO QAQuestion VALUES ('�����');
INSERT INTO QAQuestion VALUES ('������');
INSERT INTO QAQuestion VALUES ('������ �����');
INSERT INTO QAQuestion VALUES ('���� �����');
INSERT INTO QAQuestion VALUES ('���');
INSERT INTO QAQuestion VALUES ('������ ����');
GO
-- SO FAR EVERYTHING OK

--QA - ���� - ���� ����� ���� ���� ����� ������ ���� ��"�
INSERT INTO QA VALUES ('1','1','0','0');
INSERT INTO QA VALUES ('2','2','1','1');
INSERT INTO QA VALUES ('3','5','1','0');
INSERT INTO QA VALUES ('4','7','0','0');
INSERT INTO QA VALUES ('5','1','0','0');
INSERT INTO QA VALUES ('6','3','1','1');
GO

--Picture
INSERT INTO Picture VALUES (
'����� ��� �����',
'2013-12-31',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'1'
);

INSERT INTO Picture VALUES (
'����� ��� �����',
'2010-10-23',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'2'
);

INSERT INTO Picture VALUES (
'����� ���� ����',
'2014-06-11',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'3'
);

INSERT INTO Picture VALUES (
'����� ������ ������',
'2010-12-21',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'4'
);

INSERT INTO Picture VALUES (
'����� ������ �����',
'2011-10-22',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'5'
);

INSERT INTO Picture VALUES (
'��� ��� 1',
'2012-09-12',
'http://proj.ruppin.ac.il/igroup9/prod/images/window.jpg',
'6'
);
GO

--Pin
INSERT INTO Pin VALUES (
'30.03',
'43.12',
'���! �� ������ ������ ���',
'audio/sample1.mp3',
'video/sample1.avi',
'1'
);

INSERT INTO Pin VALUES (
'89.9',
'55.52',
'��� ����! ������ ����',
'audio/sample2.mp3',
'video/sample2.avi',
'2'
);

INSERT INTO Pin VALUES (
'35.21',
'49.17',
'�� ����� ����� �����',
'audio/sample3.mp3',
'video/sample3.avi',
'3'
);

INSERT INTO Pin VALUES (
'27.04',
'56.12',
'���� ���',
'audio/sample4.mp3',
'video/sample4.avi',
'4'
);

INSERT INTO Pin VALUES (
'82.9',
'57.52',
'���� �����',
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