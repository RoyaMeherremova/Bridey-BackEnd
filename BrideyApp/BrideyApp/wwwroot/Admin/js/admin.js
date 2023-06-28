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

})


