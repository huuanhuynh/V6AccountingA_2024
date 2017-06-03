!function (Const) {
    'use strict';

    var module = angular.module(Const.AppModule),
        link = function ($scope, element, attrs) {
            var jqElem = $(element),
                //jsParent = jqElem.closest('.flex-input-wrapper'),
                jsOver = jqElem.clone(),
                pos = jqElem.offset(),
                oldWidth = jqElem.width() + 'px',
                newWidth = jsOver.attr('data-exp-width') || '300px',
                duration = jsOver.attr('data-exp-duration') || '500ms';

            jsOver
                .val(jqElem.val())
                //.removeAttr('class')
                .removeAttr('id')
                .removeAttr('name')
                //.removeAttr('spellcheck')
                .removeAttr('style')
                .css({
                    position: 'absolute',
                    width: oldWidth,
                    height: jqElem.outerHeight(),
                    padding: jqElem.css('padding'),
                    margin: jqElem.css('margin'),
                    'border-width': jqElem.css('border-width'),
                    'border-color': 'transparent',
                    'z-index': 999,
                    transition: 'width ' + duration + ' ease',
                    display: 'none'
                })
                .on('blur', function () {
                    jsOver
                        .on('transitionend', function () {
                            var val = jsOver.val();
                            if (jqElem.hasClass('tt-input')) {
                                jqElem.typeahead('val', val);
                            } else {
                                jqElem.val(jsOver.val());
                            }
                            jsOver.off('transitionend').hide();
                        })
                        .width(oldWidth);
                });
            $(document.body).append(jsOver);

            jqElem.on('focus', function () {
                //let jsOrigin = $(this),
                //    pos = jsOrigin.offset(),
                //    val = jsOrigin.val();
                //jsOver = $('<input class="flex-input-over" type="text" />');

                var pos = jqElem.offset();

                jsOver
                    .val(jqElem.val())
                    .css({
                        top: pos.top,
                        left: pos.left,
                    })
                    .show();

                jsOver.focus().width(newWidth);
            });
        };

    module.directive('v6FlexInput', ['$rootScope', '$state',
        function ($rootScope, $state) {
            return {
                replace: true,
                restrict: 'AE',
                template: '<input />',
                link: function ($scope, elem, attrs) {
                    link($scope, elem, attrs);
                }
            };
        }
    ]);
}(Const);