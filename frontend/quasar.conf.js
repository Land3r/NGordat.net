// Configuration for your app
// const env = require('quasar-dotenv').config()
let path = require('path')

module.exports = function (ctx) {
  return {
    // app boot file (/src/boot)
    // --> boot files are part of "main.js"
    boot: [
      'i18n', // App internationalisation
      'axios', // HTTP request
      'vuelidate' // Form validation
    ],

    css: [
      'app.styl'
    ],

    extras: [
      'roboto-font',
      'material-icons' // optional, you are not bound to it
      // 'ionicons-v4',
      // 'mdi-v3',
      // 'fontawesome-v5',
      // 'eva-icons'
    ],

    framework: {
      // all: true, // --- includes everything; for dev only!

      components: [
        // Layout
        'QLayout',
        'QPageContainer',
        'QPage',
        'QHeader',
        'QFooter',
        'QDrawer',
        'QPageSticky',
        'QPageScroller',
        'QToolbar',
        'QToolbarTitle',
        // Form
        'QInput',
        // Misc
        'QAvatar',
        'QBadge',
        'QBtn',
        'QSeparator',
        'QIcon',
        'QBreadcrumbs',
        'QBreadcrumbsEl',
        // Cards,
        'QCard',
        'QCardSection',
        'QCardActions',
        // Items
        'QList',
        'QItem',
        'QItemSection',
        'QItemLabel',
        // Animations
        'QSlideTransition'
      ],

      directives: [
        'Ripple', // Ripple effect on click
        'ClosePopup' // For closing popups and dialogs
      ],

      // Quasar plugins
      plugins: [
        'Notify', // For notifications
        'Loading', // For global website loader
        'Dialog', // For dialogs popups
        'AddressbarColor' // For customizing the address bar for mobile browsers
      ]

      // iconSet: 'ionicons-v4'
      // lang: 'de' // Quasar language
    },

    supportIE: false,

    build: {
      scopeHoisting: true,
      // vueRouterMode: 'history',
      // vueCompiler: true,
      // gzip: true,
      // analyze: true,
      // extractCSS: false,
      // env: env, // dotenv using quasar-dotenv
      extendWebpack (cfg) {
        // Adds eslint to webpack build.
        cfg.module.rules.push({
          enforce: 'pre',
          test: /\.(js|vue)$/,
          loader: 'eslint-loader',
          exclude: /node_modules/
        })
        // Adds require and import aliases
        // Add webpack aliases
        cfg.resolve.alias = {
          ...cfg.resolve.alias,
          '@': path.resolve(__dirname, 'src'),
          'assets': path.resolve(__dirname, 'src/assets'),
          'boot': path.resolve(__dirname, 'src/boot'),
          'components': path.resolve(__dirname, 'src/components'),
          'data': path.resolve(__dirname, 'src/data'),
          'domain': path.resolve(__dirname, 'src/domain'),
          'helpers': path.resolve(__dirname, 'src/helpers'),
          'i18n': path.resolve(__dirname, 'src/i18n'),
          'layouts': path.resolve(__dirname, 'src/layouts'),
          'pages': path.resolve(__dirname, 'src/pages'),
          'router': path.resolve(__dirname, 'src/router'),
          'services': path.resolve(__dirname, 'src/services'),
          'store': path.resolve(__dirname, 'src/store')
        }
      }
    },

    devServer: {
      https: true,
      // port: 8080,
      open: true // opens browser window automatically
    },

    // animations: 'all' --- includes all animations
    animations: 'all',

    ssr: {
      pwa: false
    },

    pwa: {
      // workboxPluginMode: 'InjectManifest',
      // workboxOptions: {},
      manifest: {
        // name: 'Quasar App',
        // short_name: 'Quasar-PWA',
        // description: 'Best PWA App in town!',
        display: 'standalone',
        orientation: 'portrait',
        background_color: '#ffffff',
        theme_color: '#027be3',
        icons: [
          {
            'src': 'statics/icons/icon-128x128.png',
            'sizes': '128x128',
            'type': 'image/png'
          },
          {
            'src': 'statics/icons/icon-192x192.png',
            'sizes': '192x192',
            'type': 'image/png'
          },
          {
            'src': 'statics/icons/icon-256x256.png',
            'sizes': '256x256',
            'type': 'image/png'
          },
          {
            'src': 'statics/icons/icon-384x384.png',
            'sizes': '384x384',
            'type': 'image/png'
          },
          {
            'src': 'statics/icons/icon-512x512.png',
            'sizes': '512x512',
            'type': 'image/png'
          }
        ]
      }
    },

    cordova: {
      // id: 'org.cordova.quasar.app'
    },

    electron: {
      // bundler: 'builder', // or 'packager'

      extendWebpack (cfg) {
        // do something with Electron main process Webpack cfg
        // chainWebpack also available besides this extendWebpack
      },

      packager: {
        // https://github.com/electron-userland/electron-packager/blob/master/docs/api.md#options

        // OS X / Mac App Store
        // appBundleId: '',
        // appCategoryType: '',
        // osxSign: '',
        // protocol: 'myapp://path',

        // Window only
        // win32metadata: { ... }
      },

      builder: {
        // https://www.electron.build/configuration/configuration

        // appId: 'quasar-app'
      }
    }
  }
}
