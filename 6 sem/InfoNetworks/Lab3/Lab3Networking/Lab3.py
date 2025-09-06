import time

from Members import Member, HackerMember
from Middleware import FakeIpAddressMiddleware, RSTMiddleware
from Connection import ConnectionHijack, Connection


def print_package(package):
    try:
        time.sleep(1.5)
    except KeyboardInterrupt:
        raise BaseException("Interrupted by user")

    print("[NEXT PCKG] {}".format(package))


def initialize_network():
    client = Member(777, 77)
    server1 = Member(999, 99)
    server2 = HackerMember(555, 55)
    return client, server1, server2


def initialize_middlewares():
    #tcp_reset_middleware = TcpResetMiddleware()
    fake_ip_address_middleware = FakeIpAddressMiddleware(555, 55)
    rst_middleware = RSTMiddleware()
    connection_hijack = ConnectionHijack()
    return [rst_middleware]


def create_connection(client, server1, server2, middlewares):
    connection = Connection([client, server1, server2], middlewares)
    return connection


def run_attacks():
    client, server1, server2 = initialize_network()
    middlewares = initialize_middlewares()
    connection = create_connection(client, server1, server2, middlewares)

    client.callAnyOther(connection)


run_attacks()
