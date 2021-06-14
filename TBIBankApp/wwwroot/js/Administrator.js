function BanUser(value) {
    var buttonId = "banUser" + value;
    console.log(buttonId);
    $.ajax({
        type: "POST",
        url: "/Administrator/BanUnban",
        data: {
            email: value
        },
        dataType: 'JSON',
        success: function (returndata) {
            var button = document.getElementById(buttonId);
            if (button.textContent == "Unban") {
                button.style.backgroundColor = "red";
                button.textContent = "Ban";
            }
            else {
                button.style.backgroundColor = "green";
                button.textContent = "Unban";
            }
            
        },
        error: function (response) {

        }
    })
}
