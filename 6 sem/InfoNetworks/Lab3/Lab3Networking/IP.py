class IP:

    def __init__(self, source_ip, destination_ip, payload):
        self.version = 4
        self.ihl = 5
        self.dscp = None
        self.ecn = None
        self.total_length = 576  # let it be 576 to not fragment package
        self.id = None
        self.flags = None
        self.fragment_offset = None
        self.ttl = 10000  # let it be max
        self.protocol = 6  # tcp code
        self.checksum = None  # ignore in ip
        self.source_ip = source_ip
        self.destination_ip = destination_ip
        self.payload = payload
