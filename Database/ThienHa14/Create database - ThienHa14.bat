
echo "--[Create tables]--"
sqlcmd -E -S . -i "Structure/dbo/ModelDefinitions.Table.sql"
sqlcmd -E -S . -i "Structure/dbo/Menu.Table.sql"
sqlcmd -E -S . -i "Structure/dbo/ALkh.AlterTable.sql"
sqlcmd -E -S . -i "Structure/dbo/ALnhkh.AlterTable.sql"
sqlcmd -E -S . -i "Structure/dbo/AM81.AlterTable.sql"

echo "--[Create procedures]--"
sqlcmd -E -S . -i "Stored procedures/dbo/GenerateModelDefinition.Proc.sql"
sqlcmd -E -S . -i "Stored procedures/dbo/GetModelDefinitions.Proc.sql"
sqlcmd -E -S . -i "Stored procedures/dbo/GetModelNames.Proc.sql"
sqlcmd -E -S . -i "Stored procedures/dbo/GetMenuItems.Proc.sql"
sqlcmd -E -S . -i "Stored procedures/dbo/GetMenuChildren.Proc.sql"

echo "--[Insert data]--"
sqlcmd -E -S . -i "Data/dbo/ModelDefinitions.Data.sql"
sqlcmd -E -S . -i "Data/dbo/Menu.Data.sql"
sqlcmd -E -S . -i "Data/dbo/ALnhkh.Data.sql"
pause