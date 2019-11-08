//$('#listinvalid').click(function (e) {
//    console.log(15);
//    let page = 1;
//    let status = this.attributes
//    data = {
//        'id': page,
//        'emailStatus': 'notreviewed'
//    }
//    $.ajax(
//        {
//            type: 'Get',
//            url: '/Email/ListEmails',
//            data: data,
//            success: function (data) {
//                $('#main-container').closest('#html').html(data);
//            }
//        })
//}
//)
$('document').ready(function GFG_click(clicked) {
    //console.log(clicked);
    //let button = document.getElementById(clicked);
    //button.toggle();
    console.log(clicked);
    $(`#box #${clicked}`).toggle()
    //$("#00539b02-8eab-4c4f-a5ad-fe0eff5794d5").click(function () {
    //    $("#box form").toggle();
    //    return false;
    //})
});