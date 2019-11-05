
$('.login100-form-btn').click(
    function (e) {
        let username = $('#inputUserName').val();
        let password = $('#inputPassword').val();
        let data = {
            'username': username,
            'password':password
        }
        console.log(username);
        if (!username) {
            $('#inputUserName').addClass('border', 'solid red 2px');
            $('#focus-UserName').text('Please enter username');
            e.preventDefault();
        }
        if (username) {
            $('#focus-UserName').text('');
        }
        if (!password) {
            $('#inputPassword').addClass('border', 'solid red 2px');
            $('#focus-Password').text('Please enter password');
            e.preventDefault();
        }
        if (password) {
            $('#focus-Password').text('');
        }
        //$.ajax(
        //    {
        //        type: "POST",
        //        url: "/Home/CheckForUserNameAndPassowrd",
        //        data: data,
        //        dataType: 'json',
        //        success: function (returndata) {
        //            if (returndata === "true") {
        //                $('#inputPassword').text('Invalid username or password!');
        //            }
        //            else {
        //                $('#inputPassword').text('');
        //            }
        //        }
        //    })
    })
