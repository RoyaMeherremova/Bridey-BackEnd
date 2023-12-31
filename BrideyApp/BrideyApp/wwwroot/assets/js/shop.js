//filter

let minValue = document.getElementById("min-value");
let maxValue = document.getElementById("max-value");

function validateRange(minPrice, maxPrice) {
  if (minPrice > maxPrice) {
    // Swap to Values
    let tempValue = maxPrice;
    maxPrice = minPrice;
    minPrice = tempValue;
  }

  minValue.innerHTML = "$" + minPrice;
  maxValue.innerHTML = "$" + maxPrice;
}

const inputElements = document.querySelectorAll(".range-slider input");
inputElements.forEach((element) => {
  element.addEventListener("change", (e) => {
    let minPrice = parseInt(inputElements[0].value);
    let maxPrice = parseInt(inputElements[1].value);

    validateRange(minPrice, maxPrice);
  });
});

validateRange(inputElements[0].value, inputElements[1].value);




$(document).ready(function () {
  $(".filter-toggle").on("click", function () {
    $("#overlay").removeClass("d-none");
    $(".products-variants-responsive").css("transform", `translateX(${0}px)`);
    $("body").css("overflow-y", 'hidden');
  })


  $(".close").on("click", function () {
    $("#overlay").addClass("d-none");
    $(".products-variants-responsive").css("transform", `translateX(${-239}px)`);
    $("body").css("overflow", 'scroll');

  })
  $("#overlay").on("click", function () {
    $("#overlay").addClass("d-none");
    $(".products-variants-responsive").css("transform", `translateX(${-239}px)`);
    $("body").css("overflow", 'scroll');
  })
    //all-products
    function getProductsById(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
            e.preventDefault();
        
            let id = $(this).attr("data-id");
            let data = { id: id };
            let parent = $(".productss-area")
            $.ajax({
                url: url,
                type: "Get",
                data: data,
                success: function (res) {
                    debugger
                    $(parent).html(res);
                }
            })
        })

    }

    getAllProducts(".all-product", "/Shop/GetAllProducts")

    function getAllProducts(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
            e.preventDefault();
            let parent = $(".productss-area")
            $.ajax({
                url: url,
                type: "Get",
                success: function (res) {
                    $(parent).html(res);
                }
            })
        })

    }

    getProductsById(".category", "/Shop/GetProductsByCategory")
    getProductsById(".color", "/Shop/GetProductsByColor")
    getProductsById(".composition", "/Shop/GetProductsByComposition")
    getProductsById(".size", "/Shop/GetProductsBySize")
    getProductsById(".brand", "/Shop/GetProductsByBrand")

    //FILTER
    $(document).on("submit", "#filterForm", function (e) {
        e.preventDefault();
        let value1 = $(".min-price").val();
        let value2 = $(".max-price").val();
        let data = { value1: value1, value2: value2 }
        let parent = $(".productss-area");

        $.ajax({
            url: "/Shop/GetRangeProducts",
            type: "Get",
            data: data,
            success: function (res) {
               
                $(parent).html(res);
              
                if (value1 == "10" && value2 == "500") {
                    $(".shop-navigation").addClass("d-none")
                }
              
            }
         
        })
    })

    //FILTER-RESPONSIVE
    $(document).on("submit", "#filterForm-responsive", function (e) {
        e.preventDefault();
        let value1 = $(".min-price-responsive").val();
        let value2 = $(".max-price-responsive").val();
        let data = { value1: value1, value2: value2 }
        let parent = $(".productss-area");

        $.ajax({
            url: "/Shop/GetRangeProducts",
            type: "Get",
            data: data,
            success: function (res) {

                $(parent).html(res);

                if (value1 == "10" && value2 == "500") {
                    $(".shop-navigation").addClass("d-none")
                }

            }

        })
    })
    //SORT
    $(document).on("change", "#sort", function (e) {
        e.preventDefault();
        let sortValue = $(this).val();
        console.log(sortValue)
        let data = { sortValue: sortValue };
        let parent = $(".productss-area");

        $.ajax({
            url: "/Shop/Sort",
            type: "Get",
            data: data,
            success: function (res) {
                $(parent).html(res);

            }

        })
    })

    //SORT-RESPONSIVE
    $(document).on("change", "#sort-responsive", function (e) {
        e.preventDefault();
        let sortValue = $(this).val();
        let data = { sortValue: sortValue };
        let parent = $(".productss-area");

        $.ajax({
            url: "/Shop/Sort",
            type: "Get",
            data: data,
            success: function (res) {
                $(parent).html(res);

            }

        })
    })


});



