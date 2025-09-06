help: ## Show usage and recipes (default)
	@printf "Usage: make <recipe> [...]\n"
	@printf "\nHere is the list of recipes for you:\n"
	@awk 'BEGIN {FS = ":.*?## "} /^[a-zA-Z_-]+:.*?## / {printf "  \033[36m%-20s\033[0m %s\n", $$1, $$2}' Makefile

clean: ## Clean all obj and .vs folders
	@echo Cleaning Visual Studio Craps...
	rm -Rf ./src/obj
	rm -Rf .vs

build: ## Build .NET project inside src
	@echo Building source code...
	cd src && dotnet build --nologo && cd ..

release: ## Build, Publish and zip based on pubxml
	@echo Publish source code...
	dotnet publish --nologo -c Release /p:PublishProfileFullPath='$(shell pwd)\src\Properties\Release.pubxml'
	tar -a -cf '$(shell pwd)\bin\Publish\sMkTaskManager.zip' -C '$(shell pwd)\bin\Publish' *.exe

.DEFAULT_GOAL := help
.PHONY: help clean build
