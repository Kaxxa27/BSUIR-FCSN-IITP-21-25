--ALTER SYSTEM SET COMMON_USER_PREFIX = '' SCOPE=SPFILE;

drop user dev;
drop user prod;
drop user labuser;

create user dev identified by password;
create user prod identified by password;
create user labuser identified by password;


GRANT CREATE SESSION TO dev;
GRANT CREATE SESSION TO prod;
GRANT CREATE SESSION TO labuser;

grant sysdba to LABUSER container=all;
GRANT ALL PRIVILEGES TO labuser;
SELECT username FROM all_users;