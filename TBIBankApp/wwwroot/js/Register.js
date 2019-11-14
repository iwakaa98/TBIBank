$('#RegisterButton').click(
    function (e) {
        let password = $('#password').val();
        let confirmpassword = $('#confirm-password').val();
        let username = $('#your-name').val();
        let email = $('#your-email').val();
        let flag = 0;
        let data =
        {
            'Email': email,
            'UserName': username
        }
        if (!(password === confirmpassword)) {
            $('#ConfirmPassword').text('Password does not match!');
            flag = 1;
        }
        else {
            $('#ConfirmPassword').text('');
        }
        if (!email) {
            $('#validate-email').text('Please enter email');
            flag = 1;
        }
        else {
            $('#validate-email').text('');
        }
        if (!username) {
            $('#validate-name').text('Please enter Username');
            flag = 1;
        }
        else {
            $('#validate-name').text('');
        }
        if (!password) {
            $('#validate-password').text('Please enter password');
            flag = 1;
        }
        else {
            $('#validate-password').text('');
        }
        if (!confirmpassword) {
            $('#ConfirmPassword').text('Please enter password');
            flag=1;
        }
        else if (password === confirmpassword){
            $('#ConfirmPassword').text('');
        }
        if (flag === 1) {
            e.preventDefault();
        }
        $.ajax(
            {
                type: "POST",
                url: "/Manager/CheckForUserAndEmailAsync",
                data: data,
                dataType: 'json',
                success: function (returndata) {
                    if (returndata === "true email") {
                        $('#validate-email').text('There is already registered user with this email!');
                        e.preventDefault();
                    }
                    //else {
                    //    $('#validate-email').text('');
                    //}
                    //if (returndata === "true user") {
                    //    $('#validate-name').text('There is already registered user with this username')
                    //    e.preventDefault();
                    //}
                    //else if (!username) {
                    //    $('#validate-name').text('Please enter username!');
                    //    e.preventDefault();
                    //}
                    //else {
                    //    $('#validate-name').text('');
                    //    e.preventDefault();
                    //}
                },
            })
    });
