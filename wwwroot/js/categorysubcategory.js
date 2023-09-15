    var categorySubcategoriesData = @Html.Raw(Json.Serialize(Model.CategorySubcategories));
    var subcategoriesData = @Html.Raw(Json.Serialize(Model.Subcategories));

    $(document).ready(function () {
        var categoryCmb = $("#categoryCmb");
        var subcategoryCmb = $("#subcategoryCmb");
        updateSubcategories(categoryCmb.val());
        categoryCmb.change(function () {
            var selectedCategoryId = parseInt(categoryCmb.val());
            updateSubcategories(selectedCategoryId);
        });
    });


    function updateSubcategories(selectedCategoryId) {
        var subcategoryCmb = $("#subcategoryCmb");
        subcategoryCmb.empty();
        var subcategoryIds = categorySubcategoriesData[selectedCategoryId];
        if (subcategoryIds && subcategoryIds.length > 0) 
        {
            $.each(subcategoryIds, function (index, subcategoryId) 
            {
                var subcategory = subcategoriesData.find(function (s) 
                {
                    return s.id === subcategoryId;
                });

                subcategoryCmb.append("<option value='" + subcategory.id + "'>" + subcategory.name + "</option>");
            });
        }

        else 
        {
            subcategoryCmb.append("<option value=''>-</option>");
        }
    }