# ChatApplication
 
Steps to set up the ChatApplication environment:

1.Locate into the RabbitMQ Folder and run docker-compose up
	-This would up & run RabbitMQ 
	-You can login in http://localhost:15672 using guest/guest as credentials

2.Run the SQL file located in DataBase directory
	-Create the chatapplicationdb schema before run the SQL script
	-Update your credentials in appsettings.json file to connect to the database 

3.Run the project and if take much time to show use the /Login path