
echo "--[Create database]--"
sqlcmd -E -S . -i "Structure/V6Assistant.Database.sql"

echo "--[Create roles]--]"
sqlcmd -E -S . -i "Structure/db_v6admin.Role.sql"
sqlcmd -E -S . -i "Structure/db_v6junior.Role.sql"
sqlcmd -E -S . -i "Structure/db_v6senior.Role.sql"

echo "--[Create schemas]--"
sqlcmd -E -S . -i "Structure/v6soft/Schema.sql"

echo "--[Create tables]--"
sqlcmd -E -S . -i "Structure/v6soft/Language.Table.sql"
sqlcmd -E -S . -i "Structure/v6soft/LocalizedLabel.Table.sql"
sqlcmd -E -S . -i "Structure/v6soft/LocalizedMessage.Table.sql"

echo "--[Create procedures]--"

echo "--[Insert data]--"
sqlcmd -E -S . -i "Data/v6soft/Language.Data.sql"
sqlcmd -E -S . -i "Data/v6soft/LocalizedLabel.Data.sql"
sqlcmd -E -S . -i "Data/v6soft/LocalizedMessage.Data.sql"
pause