$(function () {
    $('body').on('click', 'label.sizeItem', function (e) {
        e.preventDefault();

        var selectedSize = e.target.innerText;
        var url = "/Basket/AddToBasketWithSize";
        var Article = $(this).data("id");
        $.get(url, { clothesId: Article, size: selectedSize }, function (data) {
            $(".basket").html(data);
            var modal = document.getElementById("sizeModal");
            modal.style.display = "none";
        })
    })
});

$(function () {
    $("span.close").click(function (e) {
        e.preventDefault();
        var modal = document.getElementById("sizeModal");
        modal.style.display = "none";
    })
});

$(window).click(function (event) {
    var modal = document.getElementById("sizeModal");
    if (event.target == modal) {
        modal.style.display = "none";
    }
});