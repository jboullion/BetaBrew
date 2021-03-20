# Project Variables

PROJECT_NAME ?= BetaBrew
ORG_NAME ?= BetaBrew
REPO_NAME ?= BetaBrew

# Look for these commands
.PHONY: migrations db hello

migrations:
	cd ./BetaBrew.Data && dotnet ef --startup-project ../BetaBrew.Web/ migrations add $(mname) && cd ..

db:
	cd ./BetaBrew.Data && dotnet ef --startup-project ../BetaBrew.Web/ database update && cd ..

hello:
	echo 'Hello!'