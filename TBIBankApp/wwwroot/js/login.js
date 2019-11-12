$('.login100-form-btn').click(
    function (e) {
        let username = $('#inputUserName').val();
        let password = $('#inputPassword').val();
        var a = false;
        let data = {
            'username': username,
            'password': password,
            'rememberme': false
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
        if (password && username) {
            $.ajax(
                {
                    type: "POST",
                    url: "/Home/CheckForUserNameAndPassowrd",
                    data: data,
                    dataType: 'JSON',
                    success: function (returndata) {
                        if (!returndata) {
                            $('#focus-Password').text('Invalid username or password!');
                            a = true;
                            check(a);
                        }
                        else {
                            $('#focus-Password').text('');
                        }
                    }
                })
        }

        function check(a) {
            console.log(a);
            if (a) {
                console.log(15);
                e.preventDefault();
            }
        }
    })
