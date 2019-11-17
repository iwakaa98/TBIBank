

function changePassword(currPasword, userName) {
    let isEverythingFine = true;
    let currrentPasword = $('#currPassword').val();
    let newPassword = $('#newPassword').val();
    if (currPasword !== currrentPasword) {
        isEverythingFine = false;
        $('#focus-currPassword').text('Wrong current password!');
        event.preventDefault();
    }
    else {
        $('#focus-currPassword').text('');
    }
    if (newPassword.length < 5) {
        event.preventDefault();
        $('#focus-newPassword').text('New Password must be over 5 symbols');
    }
    else {
        $('#focus-newPassword').text('');
        $.ajax(
            {
                type: 'POST',
                url: 'Home/SetNewPasswordAsync',

                data: {
                    'UserName': userName,
                    'currPassword': currPasword,
                    'newPassword': newPassword
                },
                success: function () {
                    console.log(225);
                    console.log(isEverythingFine);
                    if (isEverythingFine) {
                        window.location.replace("/Home/Privacy");
                    }
                    //else {

                    //    window.location.replace("/Home/Privacy");
                    //}
                }
            })
    }
}