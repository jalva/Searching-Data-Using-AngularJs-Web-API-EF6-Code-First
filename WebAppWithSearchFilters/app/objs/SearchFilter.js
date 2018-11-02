
function SearchFilter(filter) {
    for (var fld in filter) {
        this[fld] = filter[fld];
    }
}

SearchFilter.prototype.hasSelections = (function () {
    function _hasSelections(filter) {
        var result;
        for (var i = 0; i < filter.filterTerms.length; i++) {
            if (filter.filterTerms[i].selected) {
                result = true;
                break;

                if (filter.filterTerms[i].nestedFilters) {
                    for (var i2 = 0; i2 < filter.filterTerms[i].nestedFilters.length; i2++) {
                        result = _hasSelections(filter.filterTerms[i].nestedFilters[i2]);
                        if (result)
                            break;
                    }
                }
            }
        }
        return result          
    }

    return function () {
        return _hasSelections(this);
    }
})();

SearchFilter.prototype.updateFromQs = (function () {

    // recursive private method that visits every nested filter in this filter's terms and updates selections based on query string
    function _updateFromQs(filter, qsMap) {

        var filterQsCsv = qsMap[filter.filterQsKey];
        var selectedFilterTermIds = filterQsCsv ? filterQsCsv.split(',') : [];

        for (var i = 0; i < filter.filterTerms.length; i++) {
            var filterTerm = filter.filterTerms[i];
            filterTerm.selected = selectedFilterTermIds.indexOf(filterTerm.id.toString()) > -1;

            if (filterTerm.nestedFilters) {
                for (var i2 = 0; i2 < filterTerm.nestedFilters.length; i2++) {
                    var nestedFilter = filterTerm.nestedFilters[i2];
                    
                    // recursion
                    _updateFromQs(nestedFilter, qsMap);
                }
            }
        }
    }

    return function (qsMap) {
        _updateFromQs(this, qsMap);
    }
})();

SearchFilter.prototype.getSelectionsQs = function () {
    var qs = "";
    var map = this.getFlattenedSelectionsMap();
    for (var key in map)
        qs += key + "=" + map[key] + "&"; // to do: re-use 
    return qs;
};

SearchFilter.prototype.getFlattenedSelectionsMap = (function () {

    // recursive private method that visits every nested filter in this filter's terms and gets its selections csv
    function flatten(filter, map) {

        var selectedFilterTerms = [];

        for (var i = 0; i < filter.filterTerms.length; i++) {
            var filterTerm = filter.filterTerms[i];

            if (filterTerm.selected)
                selectedFilterTerms.push(filterTerm.id);

            if (filterTerm.nestedFilters) {
                for (var i2 = 0; i2 < filterTerm.nestedFilters.length; i2++) {
                    var nestedFilter = filterTerm.nestedFilters[i2];

                    // invoke recursively
                    flatten(nestedFilter, map);
                }
            }
        }

        if(!map[filter.filterQsKey])
            map[filter.filterQsKey] = selectedFilterTerms.join(',');
    }

    return function () {
        var map = {};
        flatten(this, map);
        console.log('obtained map ' + map);
        return map;
    }
})();
