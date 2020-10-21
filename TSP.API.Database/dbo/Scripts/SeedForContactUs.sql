if not exists(select 1 from dbo.ContactUs) 
BEGIN

INSERT INTO dbo.ContactUs(email, [Name], [message]) values ('arthur@gmail.com', 'Arthur', 'message 0')


END