"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notification").build();

//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;

connection.on("LockButton", function (id) {
    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    //var encodedMsg = user + " says " + msg;
    //var li = document.createElement("li");
    //li.textContent = encodedMsg;
    //document.getElementById("messagesList").appendChild(li);
    let tr = document.getElementById(id + '+classID');
    let butTestDisable = document.getElementById(id);
    $(tr).css('background-color', 'red');
    butTestDisable.setAttribute("disabled", true);
    $(butTestDisable).css('background-color', '#FF0000');
    $(butTestDisable).text('Denied');
});

connection.on("UnlockButton", function (id) {
    let tr = document.getElementById(id + '+classID');
    let butTestDisable = document.getElementById(id);
    $(tr).css('background-color', 'white');
    $(butTestDisable).removeAttr("disabled");
    $(butTestDisable).css('background-color', '#007bff');
    $(butTestDisable).text('Preview');
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});
function samename(id) {
    connection.invoke("SendMessage", id);
    ///*document.getElementById(id).addEventListener("click", function (event)*/ {
    ////var user = document.getElementById("userInput").value;
    ////var message = document.getElementById("messageInput").value;
    //// SendMessage --> name of the method in C# Hub
    //connection.invoke("SendMessage", id).catch(function (err) {
    //    return console.error(err.toString());
    //});
    //event.preventDefault();
};

