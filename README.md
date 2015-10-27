# Multiplication.WebAPI
The web service part of the app I made for my nephew for multiplication practice.

Find and replace "YOURCONNECTIONSTRING" for a mysql database and it's ready to deploy. It uses entity framework for mysql.

This web service creates a question with selected difficulty, sends it to the mobile game, keeps records of questions and their solve times, student score etc.
API documentation available at URL/help.

There's also an admin panel at URL/admin. Admin user can see the sortable list of all created questions and their solve time to help prevent the student from cheating.
Admin user can also set difficulty which is used by the mobile game in real time. Admin can also reset the score of student.

<img src="http://i.imgur.com/yKkPtcU.png"/>

<img src="http://i.imgur.com/cVyxuWg.png"/>

<img src="http://i.imgur.com/TlV9FKJ.png"/>

Here is the mobile game part of the app: https://github.com/erhanalankus/Multiplication.Unity3d.Android


