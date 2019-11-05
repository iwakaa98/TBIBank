$('#RegisterButton').click(
        function (e) {
        let password = $('#password').val();
    let confirmpassword = $('#confirm-password').val();
    let username = $('#your-name').val();
    let email = $('#your-email').val();
            if (!(password === confirmpassword)) {
        //$('#ConfirmPassword').addClass('border', 'solid red 5px')
        $('#ConfirmPassword').text('Password does not match!');
    e.preventDefault();
}
            else {
        $('#ConfirmPassword').text('');
    }
            if (!username) {
        $('#validate-name').text('Please enter username!');
    e.preventDefault();
}
            else {
        $('#validate-name').text('');
    }
            if (!email) {
        $('#validate-email').text('Please enter email');
    }
            else {
        $('#validate-email').text('');
    }
});
