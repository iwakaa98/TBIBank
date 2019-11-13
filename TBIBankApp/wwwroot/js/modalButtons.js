let butTestDisable;
$(document).ready(function () {
    $('#example').DataTable();
});
function ModalTest(id) {
    $(`.${id}`).modal('show');
}

function ChekForDisable(id) {
    let thirtyminutes = 20;
    butTestDisable = document.getElementById(id);
    $.ajax(
        {
            type: "GET",
            url: "IsItOpen",
            data:
            {
                'id': id,
            },
            success: function (response) {
                if (response === "true") {
                    butTestDisable.setAttribute("disabled", true);
                    $(butTestDisable).css('background-color', 'red');
                    $(butTestDisable).text('Denied');
                }
                else {
                   
                    $(`.${id}`).modal('show');
                    starttimer(thirtyminutes, id);
                    
                }
            }
        })
}
function starttimer(duration, id) {
    console.log(duration);
    let idName = id + '+timerId';
    console.log(idName);
    let timer = duration, minutes, seconds;
    if (duration != 0) {

        setInterval(function () {
            if (timer == -1) {
                $(butTestDisable).removeAttr("disabled");
                return;
            }
            minutes = parseInt(timer / 60, 10);
            seconds = parseInt(timer % 60, 10);

            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;
            console.log(timer);
            document.getElementById(idName).innerHTML = "You have " + minutes + "m " + seconds + "s " + "before clsoing automaticly"
            if (--timer < 0) {
                SetButtonToEnable(id);
                $(`.${id}`).modal('hide');
                //return starttimer(0, id);
            }
        }, 1000);
    }
}
stoptimer(function () {

}, 1000);
function SetButtonToEnable(id) {
    butTestDisable = document.getElementById(id);
    $.ajax(
        {
            type: "Get",
            url: "SetToEnable",
            data:
            {
                'id': id,
            },
            success: function () {
                $(butTestDisable).removeAttr("disabled");
                //butTestDisable.setAttribute("disabled", false);
                $(butTestDisable).css('background-color', '#007bff');
                $(butTestDisable).text('Body Preview');

            }

        })
}
function SetInvalid(value) {
    let but = document.getElementById(value + '+classID');
    console.log(but);
    but.remove();
    data = {
        id: value,
        status: 'InvalidApplication'
    };
    $.ajax(
        {
            type: "Get",
            url: "ChangeStatus",
            data: data,
            success: function () {
            }
        })
};


function SetNew(value) {
    console.log(value)
    let but = document.getElementById(value + '+classID');
    but.remove();
    data = {
        id: value,
        status: 'New'
    };
    $.ajax(
        {
            type: "Get",
            url: "ChangeStatus",
            data: data,
            success: function () {
            }
        })
}

function SetClosed(value) {
    console.log(value)
    let but = document.getElementById(value + '+classID');
    but.remove();
    data = {
        id: value,
        status: 'Closed'
    };
    $.ajax(
        {
            type: "Get",
            url: "ChangeStatus",
            data: data,
            success: function () {
            }
        })
}
function SetNotReviewed(value) {
    console.log(value)
    let but = document.getElementById(value + '+classID');
    but.remove();
    data = {
        id: value,
        status: 'NotReviewed'
    };
    $.ajax(
        {
            type: "Get",
            url: "ChangeStatus",
            data: data,
            success: function () {
            }
        })
}
function SetOpen(value) {
    console.log(value)
    let but = document.getElementById(value + '+classID');
    but.remove();
    data = {
        id: value,
        status: 'Open'
    };
    $.ajax(
        {
            type: "Get",
            url: "ChangeStatus",
            data: data,
            success: function () {
            }
        })
}
