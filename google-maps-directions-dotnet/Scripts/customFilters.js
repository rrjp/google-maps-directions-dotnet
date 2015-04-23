/* API returns text with HTML tags in it. This filter removes the tags. */
angular.module('customFilters', []).
  filter('htmlToPlaintext', function() {
    return function(text) {
      return String(text).replace(/<[^>]+>/gm, '');
    }
  }
);