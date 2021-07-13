module.exports = {
    root: false,
    env: {
        node: true
    },
    extends: [
        'plugin:vue/essential',
        'plugin:vue/strongly-recommended',
        'plugin:vue/recommended',
        '@vue/standard'
    ],
    parserOptions: {
        parser: 'babel-eslint'
    },
    rules: {
        'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
        'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
        'vue/max-attributes-per-line': [
            2,
            {
                multiline: {
                    allowFirstLine: true
                }
            }
        ],
        'vue/html-closing-bracket-newline': ['error', {
            singleline: 'never',
            multiline: 'never'
        }],
        'max-len': ['error', { code: 140, ignoreStrings: true, ignoreUrls: true }],
        indent: ['error', 4],
        'no-shadow': 'off',
        'import/no-unresolved': 0,
        'import/extensions': 0,
        'no-unused-vars': [1, { vars: 'local', args: 'none' }],
        'linebreak-style': 0,
        'comma-dangle': 0,
        'import/prefer-default-export': 0,
        'no-unused-expressions': ['error', { allowTernary: true }],
        'no-underscore-dangle': 0,
        'no-param-reassign': 0,
        'object-curly-newline': ['error', { ObjectPattern: 'never' }],
    }
}
