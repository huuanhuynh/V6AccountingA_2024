--Tìm mã kết thúc không phải số.
Select top 1000 * from CorpLan where RIGHT(rtrim(id),1) not in ('0','1','2','3','4','5','6','7','8','9')
--Test tạo mã mới
Select RIGHT(rtrim(id),3) + SUBSTRING(id,1,len(id)-3) as new_id from Corplan where RIGHT(rtrim(id),3)='SOA'
--Update mã có MA_CT ở cuối về đầu (như đã test tạo mã)
Update Corplan set id=RIGHT(rtrim(id),3) + SUBSTRING(id,1,len(id)-3) where RIGHT(rtrim(id),3) in ('SOA','POA'...)
IND INT IXA 
'SOAASOCTSOAH00054'