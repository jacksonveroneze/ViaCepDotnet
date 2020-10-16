#!/bin/bash

rm -rf .sonarqube
find . -name '*.opencover.xml' -exec rm {} \;

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

dotnet sonarscanner begin \
/d:sonar.host.url="https://sonarcloud.io" \
/o:jacksonveroneze \
/d:sonar.login=76166eda1746980d78c96ac4c69caed0f1bb1608 \
/k:jacksonveroneze_Interest \
/d:sonar.cs.opencover.reportsPaths="src/Services/Rate/Interest.Rate.Tests/coverage.opencover.xml, src/Services/Calculator/Interest.Calculator.Tests/coverage.opencover.xml" \
/d:sonar.exclusions="src/Services/Rate/Interest.Rate.Tests/**, src/Services/Calculator/Interest.Calculator.Tests/**" \
/d:sonar.coverage.exclusions="src/Services/Rate/Interest.Rate.Tests/**, src/Services/Calculator/Interest.Calculator.Tests/**" \
/v:1.0.1

dotnet build

dotnet sonarscanner end /d:sonar.login=76166eda1746980d78c96ac4c69caed0f1bb1608
