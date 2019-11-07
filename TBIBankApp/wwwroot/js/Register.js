$('#RegisterButton').click(
    function (e) {
        let password = $('#password').val();
        let confirmpassword = $('#confirm-password').val();
        let username = $('#your-name').val();
        let email = $('#your-email').val();
        let data =
        {
            'Email': email,
            'UserName': username
        }
        if (!(password === confirmpassword)) {
            //$('#ConfirmPassword').addClass('border', 'solid red 5px')
            $('#ConfirmPassword').text('Password does not match!');
            e.preventDefault();
        }
        else {
            $('#ConfirmPassword').text('');
        }
        if (!email) {
            $('#validate-email').text('Please enter email');
            e.preventDefault();
        }
        else {
            $('#validate-email').text('');
        }
        if (!username) {
            $('#validate-name').text('Please enter Username');
            e.preventDefault();
        }
        else {
            $('#validate-name').text('');
        }
        if (!password) {
            $('#validate-password').text('Please enter password');
            e.preventDefault();
        }
        else {
            $('#validate-password').text('');
        }
        if (!confirmpassword) {
            $('#ConfirmPassword').text('Please enter password');
            e.preventDefault();
        }
        else {
            $('#ConfirmPassword').text('');
        }
        $.ajax(
            {
                type: "POST",
                url: "/Manager/CheckForUserAndEmail",
                data: data,
                dataType: 'json',
                success: function (returndata) {
                    if (returndata === "true email") {
                        $('#validate-email').addClass('border', 'solid red 2px');
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
                error: function (e) {
                    e.preventDefault();
                }

            })
    });
