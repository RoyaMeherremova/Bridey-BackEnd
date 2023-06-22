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
});