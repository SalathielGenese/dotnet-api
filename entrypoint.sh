#!/usr/env sh
set -euxo

bash ./wait-for-it.sh ${DB_HOST_PORT} --timeout=30
exec "$@"
