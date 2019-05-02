
def sum_5(func):
    return lambda a, b: func(a, b) + 5


def sum(a, b):
    return a + b

@sum_5
def sum_v2(a,b):
    return a + b

print(sum(5,10))
print(sum_v2(5, 10))