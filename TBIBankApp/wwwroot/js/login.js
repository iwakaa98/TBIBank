
$('.login100-form-btn').click(
    function (e) {
        let username = $('#inputUserName').val();
        let password = $('#inputPassword').val();
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

    })
