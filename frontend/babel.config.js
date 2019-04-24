module.exports = {
  presets: [
    '@quasar/babel-preset-app'
  ],
  plugins: [
    '@babel/plugin-proposal-optional-chaining', // ?. support
    '@babel/plugin-proposal-nullish-coalescing-operator' // ?? support
  ]
}
