/// <binding BeforeBuild='default' />
var gulp = require('gulp'),
    del = require('del'),
    browserify = require('browserify'),
    buffer = require('vinyl-buffer'),
    concat = require('gulp-concat'),
    less = require('gulp-less'),
    rename = require('gulp-rename'),
    notify = require('gulp-notify'),
    source = require('vinyl-source-stream'),
    sourcemaps = require('gulp-sourcemaps'),
    uglify = require('gulp-uglify');

// Environment plugin
var environments = require('gulp-environments'),
    development = environments.development,
    production = environments.production;

var OUT_FOLDER = './Scripts/dist';

/* 
 *    Build mode. Read more: https://www.npmjs.com/package/gulp-environments
 */
//gulp.task('set-dev', development.task);     // Build as Development mode (with source maps)
gulp.task('set-dev', production.task);    // Build as Production mode (minification, no source maps)

/****************/

gulp.task('clean-dist', [], function () {
    return del.sync(OUT_FOLDER);
});

gulp.task('bundle-vendor', [], function () {
    var BASE_DIR = './Scripts/vendor/',
        OUTPUT_FILE = 'app.vendor.js';

    var onError = function (err) {
        notify.onError({
            title: "Vendor JS",
            subtitle: "Vendor Javascript Build Failed!",
            message: "Error: <%= error.message %>"
        })(err);
        console.error(err.toString());
        this.emit('end');
    };

    return gulp.src([
                BASE_DIR + 'jquery/jquery-2.2.4.min.js',
                BASE_DIR + 'select2/select2.min.js',
                BASE_DIR + 'select2/i18n/vi.js',
                BASE_DIR + 'angular/angular.min.js',
                BASE_DIR + 'angular/i18n/angular-locale_vi-vn.min.js',
                BASE_DIR + 'angular/angular-route.min.js',
                BASE_DIR + 'angular-ui/angular-ui.min.js',
                BASE_DIR + 'angular-ui/angular-ui-router.min.js',
                BASE_DIR + 'satellizer/satellizer.min.js'
    ])
    .on('error', onError)
    .pipe(concat(OUTPUT_FILE, { sourcesContent: true }))
    .pipe(development(sourcemaps.init({ loadMaps: true })))
    .pipe(development(sourcemaps.write('./'))) // produce source map if in development mode
    .pipe(gulp.dest(OUT_FOLDER))
	.pipe(notify({
		title: 'Vendor JS',
		message: 'Vendor Javascript Completed!'
	}));
        
});

gulp.task('bundle-app', [], function () {
    var INPUT_FILE = './Scripts/app/app.main.js',
        OUTPUT_FILE = 'app.bundle.js';

    var bundler = browserify({
        extensions: ['.js', '.jsx'],
        transform: ['babelify'],
        debug: true
    });

    var onError = function (err) {
        notify.onError({
            title: "App JS",
            subtitle: "App Javascript Build Failed!",
            message: "Error: <%= error.message %>"
        })(err);
        console.error(err.toString());
        this.emit('end');
    };

    return bundler
            .add(INPUT_FILE) // Input file
            .bundle()
            .on('error', onError)
            .pipe(source(INPUT_FILE)) // converts a Browserify stream into a stream that Gulp actually understands
            .pipe(buffer()) // <----- convert from streaming to buffered vinyl file object
            .pipe(development(sourcemaps.init({ loadMaps: true })))
            .pipe(production(uglify())) // unglif if built in production mode
            .pipe(rename(OUTPUT_FILE))
            .pipe(development(sourcemaps.write('./'))) // produce source map if in development mode
            .pipe(gulp.dest(OUT_FOLDER)) // Destination FOLDER
            .pipe(notify({
                title: 'App JS',
                message: 'App Javascript Completed!'
            }));
});


gulp.task('default', ['set-dev', 'clean-dist', 'bundle-app', 'bundle-vendor']);
