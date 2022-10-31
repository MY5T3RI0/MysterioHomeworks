$(function () {

    /* Confirm sidebar deletion */

    $("a.delete").click(function () {
        if (!confirm("Confirm sidebar deletion")) return false;
    });

    /*-----------------------------------------------------------*/
});