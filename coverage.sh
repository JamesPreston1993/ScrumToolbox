dotnet test \
/p:CollectCoverage=true \
/p:Include=[ScrumToolbox.*]* \
/p:CoverletOutputFormat=opencover \
/p:CoverletOutput=../coverage/coverage.xml

reportgenerator -reports:./coverage/coverage.xml -targetdir:./coverage/report