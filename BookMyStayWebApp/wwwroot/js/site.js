
// Add your custom JavaScript code here.

function deleteConfirmation(ev) {
    ev.preventDefault();
    var urlToRedirect = ev.currentTarget.getAttribute('href')

    Swal.fire({
        title: '<h3>Are you sure?</h3>',
        color: "#d9534f",
        text: "You won't be able to revert deleted item!",
        showCancelButton: true,
        toast: true,
        showClass: {
            backdrop: 'swal2-noanimation', // disable backdrop animation
            popup: '',                     // disable popup animation
            icon: ''                       // disable icon animation
        },
        imageUrl: 'image/bms_title.svg',
        confirmButtonColor: '#0d6efd',
        cancelButtonColor: '#d9534f',
        confirmButtonText: 'Delete'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = urlToRedirect
        }
    })
    return !wait;
}
