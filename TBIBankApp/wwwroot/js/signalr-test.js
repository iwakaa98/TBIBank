﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notification").build();


connection.on("LockButton", function (id) {
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
}).catch(function (err) {
    return console.error(err.toString());
});
function samename(id) {
    connection.invoke("SendMessage", id);
};

