$('.login100-form-btn').click(
    function (e) {
        
        let username = $('#inputUserName').val();
        let password = $('#inputPassword').val();
        let data = {
            'username': username,
            'password': password
        }
        if (!username) {
            $('#focus-UserName').text('Please enter username!');
            e.preventDefault();
        }
        if (username) {
            $('#focus-UserName').text('');
        }
        if (!password) {
            $('#focus-Password').text('Please enter password!');
            e.preventDefault();
        }
        if (password) {
            $('#focus-Password').text('');
        }
        $.ajax(
            {
                type: "POST",
                url: "/Home/CheckForUserNameAndPassowrd",
                data: data,
                dataType: 'json',
                success: function (returndata) {
                    debugger;
                    if (returndata === "false") {
                        $('#focus-Password').text('Invalid username or password!');
                        e.preventDefault();
                    }
                    else {
                        $('#inputPassword').text('');
                    }
                }
            })
    })
