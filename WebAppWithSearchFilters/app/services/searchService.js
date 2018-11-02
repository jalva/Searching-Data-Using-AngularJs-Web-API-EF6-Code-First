'use strict';
app.service('searchService', [
    '$http', 'searchUrl', function ($http, searchUrl) {

        this.getByQs = function (qs) {
            return $http.get(searchUrl + '?' + qs);
        }

    }
]);