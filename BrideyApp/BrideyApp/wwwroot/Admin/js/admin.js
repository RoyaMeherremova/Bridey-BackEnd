$(function () {

    $(document).on("click", ".delete-slider", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();


        $.ajax({
            url: "slider/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-homeBanner", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();


        $.ajax({
            url: "homeBanner/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-aboutUs", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "aboutUs/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-bride", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "bride/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-position", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "position/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-team", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "team/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-advertising", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "advertising/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-aboutBanner", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "aboutBanner/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-social", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "social/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-author", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "author/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-blog", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "blog/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-aboutBox", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "aboutBox/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-size", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "size/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-color", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "color/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-composition", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "composition/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-brand", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "brand/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-category", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "category/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-product", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "product/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-image", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        $.ajax({
            url: "/Admin/Product/DeleteProductImage",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                let imagesId = $(".images").children().eq(1).attr("data-id");
                let data = $(".images").children().eq(1);
                let changeElem = $(data).children().eq(1).children().eq(1);

                if (res.id == imagesId) {
                    if ($(changeElem).children().hasClass("de-active")) {
                        $(changeElem).children().eq(0).addClass("active-status");
                        $(changeElem).children().eq(0).removeClass("de-active");
                    }
                }
            }

        })
    })
    $(document).on("click", ".image-status", function (e) {
        e.preventDefault();
        let imageId = $(this).parent().parent().attr("data-id");
        let elems = $(".image-status")
        let changeElem = $(this);
        let data = { id: imageId }
        $.ajax({
            url: "/Admin/Product/SetMainİmage",
            type: "Post",
            data: data,
            success: function (res) {
                if (res) {
                    for (var elem of elems) {
                        if ($(elem).hasClass("active-status")) {
                            $(elem).removeClass("active-status")
                            $(elem).addClass("de-active")
                        }
                    }
                    if ($(changeElem).hasClass("de-active")) {
                        $(changeElem).removeClass("de-active");
                        $(changeElem).addClass("active-status");
                    }
                }
            }
        })
    })
    $(document).on("click", ".delete-blogComment", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "blogComment/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-productComment", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "productComment/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-user", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "user/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-contact", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "contact/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })
    $(document).on("click", ".delete-subscribe", function (e) {
        e.preventDefault();
        let Id = $(this).parent().parent().attr("data-id");
        let deletedElem = $(this).parent().parent();
        let data = { id: Id }
        let tbody = $(this).parent().parent().parent();

        $.ajax({
            url: "subscribe/delete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).remove();
                if ($(tbody).length == 0) {
                    $(".table").remove();
                }
            }

        })
    })





})


