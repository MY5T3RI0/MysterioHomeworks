$(function () {

    /* Confirm sidebar deletion */

    $("a.delete").click(function () {
        if (!confirm("Confirm sidebar deletion")) return false;
    });

    $("table#sidebars tbody").sortable({
        items: "tr:not(.home)",
        placeholder: "ui-state-highlight",
        update: function () {
            var ids = $("table#sidebars tbody").sortable("serialize");
            var url = "/Admin/Sidebar/ReorderSidebars";

            $.post(url, ids, function (data) {
            });
        }
    });
    /*-----------------------------------------------------------*/
});