FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

RUN apt-get update \
    && apt-get install --no-install-recommends -y tzdata \
    && apt-get clean

ENV TZ=America/Sao_Paulo
ENV LANG pt_BR.UTF-8
ENV LANGUAGE ${LANG}
ENV LC_ALL ${LANG}

RUN ln -snf /usr/share/zoneinfo/${TZ} /etc/localtime && echo "${TZ}" > /etc/timezone

COPY . /app
RUN dotnet restore
RUN dotnet build -c Release
RUN dotnet publish -c Release -o out

COPY Data/Migrations/migrations.sql /migrations/
COPY Data/Migrations/Launch.data/seeddatabase.sh /docker-entrypoint-initdb.d/

RUN if [ -f "Data/Migrations/Launch.data/spacedevs_data.sql" ]; then \
        mkdir -p /migrations/seed/ \
        && cp Data/Migrations/Launch.data/spacedevs_data.sql /migrations/seed/; \
    fi

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/out ./

EXPOSE 5000
ENTRYPOINT ["dotnet", "Services.dll"]