ALTER TABLE V6User
ADD web_password varchar(100)
Go

UPDATE V6User
Set web_password = 'v6soft'
where user_id = 116