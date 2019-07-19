## APP BUILDER
FROM microsoft/dotnet:2.2-runtime

# Default Environment
ENV ASPNETCORE_ENVIRONMENT="Development"
ENV CURRENT_VERSION="__[Version]__"

# Args
ARG distFolder=EnvironmentApi/bin/Release/netcoreapp2.2
ARG apiProtocol=http
ARG apiPort=80
ARG appFile=EnvironmentApi.dll
 
# Copy files to /app
RUN ls
COPY ${distFolder} /app

# Expose port for the Web API traffic
ENV ASPNETCORE_URLS ${apiProtocol}://+:${apiPort}
EXPOSE ${apiPort}

# Run application
WORKDIR /app
RUN ls
ENV appFile=$appFile
ENTRYPOINT dotnet $appFile