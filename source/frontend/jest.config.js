module.exports = {
  preset: "jest-preset-angular",
  moduleNameMapper: {
    "^environments/(.*)$": "<rootDir>/src/environments/$1",
    "^src/(.*)$": "<rootDir>/src/$1",
    "^app/(.*)$": "<rootDir>/src/app/$1",
    "^assets/(.*)$": "<rootDir>/src/assets/$1",
  },
  setupFilesAfterEnv: ["<rootDir>/src/setup-jest.ts"],

  transform: {
    "^.+\\.(ts|mjs|js|html)$": "jest-preset-angular",
  },
  transformIgnorePatterns: ["node_modules/(?!.*\\.mjs$)"],
  testResultsProcessor: "jest-teamcity-reporter",
  reporters: [
    "default",
    [
      "jest-trx-results-processor",
      {
        outputFile: require("path").join(
          __dirname,
          "../../artifacts/logs/TestReport",
          "Erowa.MultiCell.Frontend.trx"
        ),
      },
    ],
  ],
};
